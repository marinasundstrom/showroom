using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;

namespace Essiq.Showroom.Client.Shared
{
    /* This is almost equivalent to a .razor file containing:
     *
     *    @inherits InputBase<string>
     *    <textarea @bind="CurrentValue" id="@Id" class="@CssClass"></textarea>
     *
     * The only reason it's not implemented as a .razor file is that we don't presently have the ability to compile those
     * files within this project. Developers building their own input components should use Razor syntax.
     */

    /// <summary>
    /// A multiline input component for editing <see cref="string"/> values.
    /// </summary>
    public class InputMarkdownEditor : InputBase<string>
    {
        private ElementReference textArea;

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        /// <inheritdoc />
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "textarea");
            builder.AddMultipleAttributes(1, AdditionalAttributes);
            builder.AddAttribute(2, "class", CssClass);
            builder.AddAttribute(3, "value", BindConverter.FormatValue(CurrentValue));
            builder.AddAttribute(4, "onchange", EventCallback.Factory.CreateBinder<string>(this, __value => CurrentValueAsString = __value, CurrentValueAsString));
            builder.AddElementReferenceCapture(5, (e) => textArea = e);
            builder.CloseElement();
        }

        /// <inheritdoc />
        protected override bool TryParseValueFromString(string value, out string result, out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }

        [JSInvokable]
        public void OnChanged(string value)
        {
            CurrentValueAsString = value;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                var obj = DotNetObjectReference.Create(this);
                await JSRuntime.InvokeVoidAsync("simpleMdeInterop.initialize", textArea, obj);
                base.OnAfterRender(firstRender);
            }
        }
    }
}
