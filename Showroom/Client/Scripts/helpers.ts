interface Window {
    helpers: {
        alert(message: string): void;
        confirm(message: string): boolean;
        prompt(message: string, _default: any): string | null;
        setTitle(title: string): void;
        scrollToTop(): void;
        scrollToBottom(): void;
        select(element: HTMLInputElement): void;
    }
}

window.helpers = {
    alert: (message) => { alert(message); },
    confirm: (message) => confirm(message),
    prompt: (message, _default) => prompt(message, _default),
    setTitle: (title) => { document.title = title; },
    scrollToTop: () => { window.scrollTo({ left: 0, top: 0, behavior: 'smooth' }); },
    scrollToBottom: () => { setTimeout(() => window.scrollTo({ left: 0, top: document.body.scrollHeight, behavior: 'smooth' }), 200); },
    select: (element) => element.select()
};