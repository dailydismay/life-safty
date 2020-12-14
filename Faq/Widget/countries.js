function Countries({countries}) {
    let elem;

    this.getElem = getElem;

    function getElem() {
        if (!elem) render();
        return elem;
    }

    function generateOption(country){
        const option = document.createElement('option');
        option.textContent = country;
        return option;
    }

    function render() {
        elem = document.createElement('select');
        elem.classList.add('widget__countries');
        elem.classList.add('form__item');

        countries.forEach(country => {
            elem.appendChild(generateOption(country));
        });
    }
}

export default Countries;