import Widget from './Widget/widget.js';

window.addEventListener('DOMContentLoaded', () => {
    const widget = new Widget({
        countries: ['Москва', 'Казань', 'Санкт-Петербург','Екатеринбург', 'Самара'],
    });

    setTimeout(() => {
        document.body.appendChild(widget.getElem())
    },);
})