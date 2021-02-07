interface Window {
    elementHelpers: {
        focus(element: HTMLInputElement): void;
        blur(element: HTMLInputElement): void;
        select(element: HTMLInputElement): void;
    },
    listbox: {
        number: number,
        removeHandlers: {
            [key: number]: Function[]
        },
        init(listbox: HTMLElement, ref: DotNet.DotNetObject): number;
        dispose(id: number): void;
    }
}

interface EventTarget {
    closest(selector: string): Element;
}

window.elementHelpers = {
    focus: (element) => element.focus(),
    blur: (element) => element.blur(),
    select: (element) => element.select()
};

window.listbox = {
    number: 0,
    removeHandlers: {},
    init: function (listbox, ref) {
        const listener = async (event: MouseEvent) => {
            if (event.target) {
                const closestListBox = event.target.closest('.listbox');
                if (listbox != closestListBox) {
                    await ref.invokeMethodAsync("Blur");
                }
            }
        };

        document.addEventListener("click", listener);

        const removers = [];
        removers.push(() => document.removeEventListener("click", listener));

        ++this.number;
        this.removeHandlers[this.number] = removers;
        return this.number;
    },
    dispose: function (id) {
        const listener = this.removeHandlers[id];
        delete this.removeHandlers[id]
        for (let l of listener)
        {
            l();
        }
    }
};