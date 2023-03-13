let navItems = document.querySelectorAll(".nav-item")
let show = false;

function toggleNav()
{
    if (!show)
    {
        navItems.forEach(i => i.style.display = "inline");
        show = true;
    }
    else
    {
        navItems.forEach(i => i.style.display = "none");
        show = false;
    }
}