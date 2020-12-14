import Countries from './countries.js';
import validateEmail from './validators/emailValidator.js';
import validatePhone from './validators/phoneValidator.js';
import css from './styles.js';

function Widget({countries}) {
    let elem;
    let error;
    let style;
    let closer;

    this.getElem = getElem;

    function getElem() {
        if (!elem) render();
        setTimeout(() => {
            elem.classList.add('active');
        }, 40)
        elem.addEventListener('submit', submit);
        closer.addEventListener('click', close);
        return elem;
    }

    function fieldGenerator(elem, root, placeholder, ...classes){
        elem.placeholder = placeholder;
        classes.forEach(clazz => {
            elem.classList.add(clazz);
        });
        root.appendChild(elem);
    }

    function closerGenerator(){
        closer = document.createElement('div');
        closer.classList.add('closer');
        return closer;
    }

    function render() {
        elem = document.createElement('form');
        elem.classList.add('widget');

        elem.appendChild(closerGenerator());

        const nameElem = document.createElement('input');
        fieldGenerator(nameElem, elem, 'ФИО', 'name', 'form__item');

        const select = new Countries({
            countries,
        }).getElem();
        
        elem.appendChild(select);

        const phoneElem = document.createElement('input');
        fieldGenerator(phoneElem, elem, 'Номер телефона', 'phone', 'form__item');

        const emailElem = document.createElement('input');
        fieldGenerator(emailElem, elem, 'Email', 'email', 'form__item');

        const submit = document.createElement('button');
        submit.classList.add('form__item');
        submit.type = 'submit';
        submit.textContent = 'Войти';
        elem.appendChild(submit);

        style = document.createElement('style');

        style.type = 'text/css';
        if (style.styleSheet){
            style.styleSheet.cssText = css;
        } else {
            style.appendChild(document.createTextNode(css));
        }
        
        document.head.appendChild(style);
    }

    function validate(phone, email){
        if(validatePhone(phone) && validateEmail(email)){
            return true;
        }
        return false;
    }

    function submit(event){
        event.preventDefault();

        const target = event.target,
              fullName = target.querySelector('.name'),
              phone = target.querySelector('.phone'),
              email = target.querySelector('.email');

        if(error){
            error.remove();
        }

        if(!validatePhone(phone.value)){
            error = document.createElement('h3');
            error.textContent = 'Неверный phone';
            elem.insertAdjacentElement('afterbegin', error);
        } else if(!validateEmail(email.value)){
            error = document.createElement('h3');
            error.textContent = 'Неверный email';
            elem.insertAdjacentElement('afterbegin', error);
        } else {
            elem.innerHTML = '<h2></h2>';
            setTimeout(() => {
                elem.remove();
            },);
        }
    }

    function close(){
        elem.classList.remove('active');
        setTimeout(() => {
            elem.remove();
        }, 1500);
    }
}

export default Widget;