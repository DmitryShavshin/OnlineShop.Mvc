const imgs = document.querySelectorAll('.img-select a');
const imgBtns = [...imgs];
let imgId = 1;

imgBtns.forEach((imgItem) => {
    imgItem.addEventListener('click', (event) => {
        event.preventDefault();
        imgId = imgItem.dataset.id;
        slideImage();
    });
});

function slideImage() {
    const displayWidth = document.querySelector('.img-showcase img:first-child').clientWidth;

    document.querySelector('.img-showcase').style.transform = `translateX(${- (imgId - 1) * displayWidth}px)`;
}

window.addEventListener('resize', slideImage);

// Display multiple images on create page

document.querySelector('#files').addEventListener("change", (e) => {
    if (window.File && window.FileList && window.FileReader && window.Blob) {
        const files = e.target.files;
        const output = document.querySelector("#result");

        for (var i = 0; i < files.length; i++) {
            if (!files[i].type.match("image")) continue;

            const picReader = new FileReader();
            picReader.addEventListener("load", function (event) {
                const picFile = event.target;
                let div = document.createElement("div");
                div.className = `galery-container`;
                div.innerHTML = `<img src="${picFile.result}" title="${picFile.name}" />`;
                output.appendChild(div);
            });
            picReader.readAsDataURL(files[i]);
        }
    } else {
        alert("Your browser does not suport the File API");
    }
});


////Add Active class
//let list = document.querySelector('.list');
//for (let i = 0; i < list.length; i++) {
//    list[i].onclick = function () {
//        let j = 0;
//        while (j < list.length) {
//            list[j++].className = 'list';
//        }
//        list[i].className = 'list active';
//    }
//}