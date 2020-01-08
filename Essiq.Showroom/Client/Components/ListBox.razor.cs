using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Essiq.Showroom.Client.Components
{
    public partial class ListBox<TValue> : ComponentBase
    {
#pragma warning disable IDE0044 // Add readonly modifier
        private ElementReference listBox;
        private ElementReference inputRef;
        private ElementReference menuDiv;
#pragma warning restore IDE0044 // Add readonly modifier
        private IEnumerable<TValue> focusedList;
        private string inputValue;
        public bool isInvalid;
        private int subscriptionId;

        private TValue selectedItemInternal;

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public TValue SelectedItem
        {
            get => selectedItemInternal;
            set
            {
                selectedItemInternal = value;
                inputValue = GetStringFromValue(value);
            }
        }

        [Parameter]
        public EventCallback<TValue> SelectedItemChanged { get; set; }

        [Parameter]
        public IReadOnlyList<TValue> Items { get; set; }

        [Parameter]
        public bool IsOpen { get; set; }

        [Parameter]
        public bool IsEditable { get; set; }

        [Parameter]
        public RenderFragment<TValue> ItemTemplate { get; set; }

        [Parameter]
        public Func<string, IEnumerable<TValue>, Task<IEnumerable<TValue>>> Query { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object> AdditionalAttributes { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                subscriptionId = await JSRuntime.InvokeAsync<int>("listbox.init", listBox, DotNetObjectReference.Create(this));
            }
        }

        public async ValueTask DisposeAsync()
        {
            await JSRuntime.InvokeVoidAsync("listbox.dispose", subscriptionId);
        }

        private async Task OnInput(ChangeEventArgs changeEventArgs)
        {
            inputValue = changeEventArgs.Value as string;

            IsOpen = true;

            await UpdateListAsync(inputValue?.ToLower());

            SetValue();

            await SelectedItemChanged.InvokeAsync(SelectedItem);
        }

        private void SetValue()
        {
            if (ParseValueFromString(inputValue, Items, out var result, out var errorMessage))
            {
                isInvalid = false;
                SelectedItem = result;
            }
            else
            {
                isInvalid = true;
                SelectedItem = default;
            }
        }

        private async Task OnItemClick(TValue item)
        {
            SelectedItem = item;

            IsOpen = false;
            isInvalid = false;

            await BlurElement(inputRef);

            await SelectedItemChanged.InvokeAsync(SelectedItem);
        }

        private async Task OnFocus(FocusEventArgs ev)
        {
            IsOpen = true;

            await UpdateListAsync(string.Empty);

            if (IsEditable)
            {
                await SelectElement(inputRef);
            }
        }

        private async Task OnKeyPress(KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case "Escape":
                    IsOpen = false;

                    await BlurElement(inputRef);
                    break;
                case "Enter":
                    IsOpen = false;
                    SetValue();
                    if (!isInvalid)
                    {
                        inputValue = GetStringFromValue(SelectedItem);
                    }
                    await SelectedItemChanged.InvokeAsync(SelectedItem);
                    break;
                case "ArrowUp":
                    {
                        IsOpen = true;

                        var index = focusedList
                            .ToList()
                            .IndexOf(SelectedItem);

                        if (index <= 0)
                        {
                            return;
                        }
                        SelectedItem = focusedList.ElementAt(index - 1);

                        inputValue = GetStringFromValue(SelectedItem);

                        await SelectElement(inputRef);
                        break;
                    }
                case "ArrowDown":
                    {
                        IsOpen = true;

                        var count = focusedList.Count();
                        var index = focusedList
                            .ToList()
                            .IndexOf(SelectedItem);

                        if (index == count - 1)
                        {
                            return;
                        }
                        SelectedItem = focusedList.ElementAt(index + 1);

                        inputValue = GetStringFromValue(SelectedItem);

                        await SelectElement(inputRef);
                        break;
                    }
            }
        }

        private async Task ToggleList()
        {
            IsOpen = !IsOpen;

            if (IsOpen)
            {
                await UpdateListAsync(string.Empty);

                if (IsEditable)
                {
                    await FocusElement(menuDiv);
                }
            }
        }

        [JSInvokable]
        public void Blur()
        {
            IsOpen = false;
            StateHasChanged();
        }

        private async Task UpdateListAsync(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                focusedList = Items;
            }
            else
            {
                if (Query != null)
                {
                    focusedList = await PopulateItemsAsync(input, Items);
                }
                else
                {
                    focusedList = Items
                        .Where(item => item.ToString().ToLower().Contains(input.ToLower()));
                }
            }
        }

        private async Task FocusElement(ElementReference elementReference)
        {
            await JSRuntime.InvokeVoidAsync("elementHelpers.focus", inputRef);
        }

        private async Task BlurElement(ElementReference elementReference)
        {
            await JSRuntime.InvokeVoidAsync("elementHelpers.blur", inputRef);
        }

        private async Task SelectElement(ElementReference elementReference)
        {
            await JSRuntime.InvokeVoidAsync("elementHelpers.select", inputRef);
        }

        protected virtual Task<IEnumerable<TValue>> PopulateItemsAsync(string query, IEnumerable<TValue> items)
        {
            return Task.FromResult(
                Items.Where(item => item.ToString().ToLower().Contains(query.ToLower())));
        }

        protected virtual bool ParseValueFromString(string str, IEnumerable<TValue> items, out TValue result, out string errorMessage)
        {
            var isNullable = typeof(TValue).IsNullable();
            var type = isNullable ? Nullable.GetUnderlyingType(typeof(TValue)) : typeof(TValue);

            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
            {
                result = default;

                if (isNullable)
                {
                    errorMessage = "";
                    return false;
                }

                errorMessage = "";
                return true;
            }

            if (type == typeof(int))
            {
                if (int.TryParse(str, out var outValue))
                {
                    result = (TValue)(object)outValue;
                }
                else
                {
                    result = default;
                    errorMessage = "";
                    return false;
                }
            }
            else
            {
                result = (TValue)(object)str;
            }

            if (items.Contains((TValue)(object)result))
            {
                errorMessage = null;
                return true;
            }

            result = default;
            errorMessage = "";
            return false;
        }

        protected virtual string GetStringFromValue(TValue value)
        {
            return value?.ToString();
        }
    }
}
