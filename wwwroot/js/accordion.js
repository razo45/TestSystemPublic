this.addEventListener('click', (e) => {
    // получим элемент .accordion__header
    const elHeader = e.target.closest('.accordion__header');
    // если такой элемент не найден, то прекращаем выполнение функции
    if (!elHeader) {
        return;
    }
    // если необходимо, чтобы всегда был открыт один элемент
    if (!this._config.alwaysOpen) {
        // получим элемент с классом accordion__item_show и сохраним его в переменную elOpenItem
        const elOpenItem = this._el.querySelector('.accordion__item_show');
        // если такой элемент есть
        if (elOpenItem) {
            // и он не равен текущему, то переключим ему класс accordion__item_show
            elOpenItem !== elHeader.parentElement ? elOpenItem.classList.toggle('accordion__item_show') : null;
        }
    }
    // переключим класс accordion__item_show элемента .accordion__header
    elHeader.parentElement.classList.toggle('accordion__item_show');
  
});

