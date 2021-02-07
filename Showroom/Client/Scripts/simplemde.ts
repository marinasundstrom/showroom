declare var SimpleMDE: any;

interface Window {
    simpleMdeInterop: {
        initialize(element: HTMLElement, obj: any): void;
    }
}

window.simpleMdeInterop = {
    initialize: (element, obj) => {
        var editor = new SimpleMDE({
            element: element,
            autosave: true,
            forceSync: true
        });

        editor.codemirror.on("change", () => {
            obj.invokeMethodAsync("OnChanged", editor.value());
        });
    },
};