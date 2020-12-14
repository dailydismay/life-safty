const css = `
.widget {
    position: fixed;
    bottom: 0;
    right: 0;
    transform: translateY(100%);

    transition: all 1.5s;

    color: black;

    padding: 35px 15px 15px 15px;
    border-radius: 10px;

    background-color: cornflowerblue;

    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
}

.widget.active {
    transform: translateY(0);
}

.form__item {
    font-size: 16px;
    font-family: sans-serif;
    font-weight: 700;
    color: black;
    line-height: 1.3;
    padding: .6em 1.4em .5em .8em;
    width: 100%;
    max-width: 100%;
    box-sizing: border-box;
    margin: 20px 0;
    border: 1px solid #aaa;
    box-shadow: 0 1px 0 1px rgba(0, 0, 0, .04);
    border-radius: .5em;
}

input, button {
    border: 0;
    outline: 0;
}

input {
    font-size: var(--font-size-small);
    opacity: 1;
    transform: translateY(0);
    transition: all 0.2s ease-out;
}

.widget__countries{
    display: block;
    -moz-appearance: none;
    -webkit-appearance: none;
    appearance: none;
    background-color: #fff;
    background-image: url('data:image/svg+xml;charset=US-ASCII,%3Csvg%20xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2Fsvg%22%20width%3D%22292.4%22%20height%3D%22292.4%22%3E%3Cpath%20fill%3D%22%23007CB2%22%20d%3D%22M287%2069.4a17.6%2017.6%200%200%200-13-5.4H18.4c-5%200-9.3%201.8-12.9%205.4A17.6%2017.6%200%200%200%200%2082.2c0%205%201.8%209.3%205.4%2012.9l128%20127.9c3.6%203.6%207.8%205.4%2012.8%205.4s9.2-1.8%2012.8-5.4L287%2095c3.5-3.5%205.4-7.8%205.4-12.8%200-5-1.9-9.2-5.5-12.8z%22%2F%3E%3C%2Fsvg%3E'), linear-gradient(to bottom, #ffffff 0%, #e5e5e5 100%);
    background-repeat: no-repeat, repeat;
    background-position: right .7em top 50%, 0 0;
    background-size: .65em auto, 100%;
}

.widget__countries::-ms-expand {
    display: none;
}

.widget__countries:hover {
    border-color: #888;
}

.widget__countries:focus {
    border-color: #aaa;
    box-shadow: 0 0 1px 3px rgba(59, 153, 252, .7);
    box-shadow: 0 0 0 3px -moz-mac-focusring;
    color: #222;
    outline: none;
}

.widget__countries option {
    font-weight: normal;
}

*[dir="rtl"] .widget__countries,
:root:lang(ar) .widget__countriess,
:root:lang(iw) .widget__countries {
    background-position: left .7em top 50%, 0 0;
    padding: .6em .8em .5em 1.4em;
}

.closer {
    width: 40px;
    height: 40px;
    border-radius: 40px;
    position: absolute;
    top: 0;
    right: 0;
    z-index: 1;
    margin: 10px 15px 0 auto;
    cursor: pointer;
}
.closer:before {
    content: '+';
    color: #337AB7;
    position: absolute;
    z-index: 2;
    transform: rotate(45deg);
    font-size: 50px;
    line-height: 1;
    top: -5px;
    left: 6px;
    transition: all 0.3s cubic-bezier(0.77, 0, 0.2, 0.85);
}
.closer:after {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    border-radius: 100%;
    background: #337AB7;
    z-index: 1;
    transition: all 0.3s cubic-bezier(0.77, 0, 0.2, 0.85);
    transform: scale(0.01);
}
.closer:hover:after {
    transform: scale(1);
}
.closer:hover:before {
    transform: scale(0.8) rotate(45deg);
    color: #fff;
}`;

export default css;