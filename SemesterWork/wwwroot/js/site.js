// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

const fetchProfile = async () => {
    const resp = await fetch('/Api/Profile')
    return resp.json()
}

const buildProfileDropdown = ({ firstName, lastName }) => `
    <ul class="uk-nav uk-dropdown-nav">
      <li class="uk-active">${firstName} ${lastName}</li>
      <li><a href="#">Профиль</a></li>
      <li class="uk-nav-divider"></li>
      <li><a href="#">Выйти</a></li>
    </ul>
`

document.onreadystatechange = async () => {
    const profile = await fetchProfile()
    if (profile) {
        
    }
}
