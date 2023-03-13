let navItems = document.querySelectorAll(".nav-item");
let navBar = document.querySelector("nav");


navBar.onmouseover = function () {
    setTimeout(()=>{navItems.forEach(i => i.style.opacity = "1.0")},400)
}
navBar.onmouseout = function () {
    setTimeout(()=>{navItems.forEach(i => i.style.opacity = "0")},100)
}