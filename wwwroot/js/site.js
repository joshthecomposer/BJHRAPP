let navItems = document.querySelectorAll(".nav-item");
let navBar = document.querySelector("nav");


navBar.onmouseenter = function () {
    setTimeout(() => {
        navItems.forEach(i => {
            i.style.opacity = "1.0"
            i.style.display = "inline"
        })
    }, 400)
}
navBar.onmouseleave = function () {
    setTimeout(() => {
        navItems.forEach(i => {
            i.style.opacity = "0"
            i.style.display = "none"
        })
    }, 100)
}