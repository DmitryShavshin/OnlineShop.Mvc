////let switchCnt = document.querySelector("#switch_cnt");
////let switchC1 = document.querySelector('#switch_c1');
////let switchC2 = document.querySelector('#switch_c2');
////let switchCircle = document.querySelector('.switch__circle');
////let switchBtn = document.querySelector('.switch__btn');
////let aContainer = document.querySelector('#a_container');
////let bContainer = document.querySelector('#b_container');
////let allButtons = document.querySelector('.submit');

////let getButtons = (e) => e.preventDefault();

////let changeForm = (e) => {
////    switchCnt.classList.add('is-gx');
////    setTimeout(function () {
////        switchCnt.classList.remove('is-gx');
////    }, 1500);

////    switchCnt.classList.toggle('is-txr');
////    switchCircle[0].classList.toggle('is-txr');
////    switchCircle[1].classList.toggle('is-txr');

////    switchC1.classList.toggle('is-hidden');
////    switchC2.classList.toggle('is-hidden');
////    aContainer.classList.toggle('is-txl');
////    bContainer.classList.toggle('is-txl');
////    bContainer.classList.toggle('is-z200');
////    console.log(bContainer.classList);
////}

////let mainF = (e) => {
////    for (var i = 0; i < allButtons.length; i++) {
////        allButtons[i].addEventListener('click', getButtons);
////    }
////    for (var i = 0; i < switchBtn.length; i++) {
////        allButtons[i].addEventListener('click', changeForm);
////        console.log(allButtons.classList);
////    }
   
////}

////window.addEventListener("load", mainF);

let switchCnt = document.querySelector("#switch_cnt");
let switchC1 = document.querySelector('#switch_c1');
let switchC2 = document.querySelector('#switch_c2');
let switchCircle = document.querySelector('.switch__circle');
let switchCircleT = document.querySelector('.switch__circle--t');
let switchBtnSignIn = document.querySelector('.switch__btn__signin');
let switchBtnSignUp = document.querySelector('.switch__btn__signup');
let aContainer = document.querySelector('#a_container');
let bContainer = document.querySelector('#b_container');
let allButtons = document.querySelector('.submit');

switchBtnSignIn.onclick = function () {
    switchCnt.classList.add('is-gx');
    switchCnt.classList.toggle('is-txr');
    switchC1.classList.toggle('is-hidden');
    switchC2.classList.toggle('is-hidden');
    aContainer.classList.toggle('is-txl');
    bContainer.classList.toggle('is-txl');
    bContainer.classList.toggle('is-z200');
    switchCircle.classList.toggle('is-txr');
    switchCircleT.classList.toggle('is-txr');
}

switchBtnSignUp.onclick = function () {
    switchCnt.classList.add('is-gx');
    switchCnt.classList.toggle('is-txr');
    switchC1.classList.toggle('is-hidden');
    switchC2.classList.toggle('is-hidden');
    aContainer.classList.toggle('is-txl');
    bContainer.classList.toggle('is-txl');
    bContainer.classList.toggle('is-z200');
    switchCircle.classList.toggle('is-txr');
    switchCircleT.classList.toggle('is-txr');
}