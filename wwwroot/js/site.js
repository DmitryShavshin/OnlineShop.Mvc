
// Sydebar
const openMenu = document.querySelector('#show-menu');
const hideMenu = document.querySelector('#hide-menu');
const sideMenu = document.querySelector('#nav-menu');

openMenu.addEventListener('click', function () {
    sideMenu.classList.add('active')
});

hideMenu.addEventListener('click', function () {
    sideMenu.classList.remove('active')
});


let menuToggle = document.querySelector('.toggle');
let navigation = document.querySelector('.navigation');
let details = document.querySelector('.details');
let create = document.querySelector('.create');

menuToggle.onclick = function () {
    menuToggle.classList.toggle('active');
    navigation.classList.toggle('active');
    details.classList.toggle('active');
}

// Navbar
$(document).ready(function () {
    var trigger = $('.hamburger'),
        overlay = $('.overlay'),
        isClosed = false;

    trigger.click(function () {
        hamburger_cross();
    });

    function hamburger_cross() {

        if (isClosed == true) {
            //overlay.hide();
            trigger.removeClass('is-open');
            trigger.addClass('is-closed');
            isClosed = false;
        } else {
            //overlay.show();
            trigger.removeClass('is-closed');
            trigger.addClass('is-open');
            isClosed = true;
        }
    }

    $('[data-toggle="offcanvas"]').click(function () {
        $('#wrapper').toggleClass('toggled');
        $('.header').toggleClass('toggled');
    });
});

// Add toggle active to dropdown menu
function menuToggle() {
    const toggleMenu = document.querySelector(".menu");
    toggleMenu.classList.toggle("active");
}

//Add active class to header
window.addEventListener("scroll", function () {
    var header = document.querySelector(".header");
    header.classList.toggle("sticky", window.scrollY > 0);
});
