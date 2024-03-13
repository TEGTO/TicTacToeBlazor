export function registerBeforeUnloadEvent(dotnetHelper) {
    window.addEventListener('beforeunload', function (event) {
        dotnetHelper.invokeMethodAsync('NotifyServerAboutTabClosing');
    });
}