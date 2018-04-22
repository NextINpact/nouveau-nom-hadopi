// Write your JavaScript code.
$(document).on('click',
    '.do-upvote',
    function (evt) {
        evt.preventDefault();
        var id = $(this).data('id');
        $.post($(this)[0].href,
            { id: id },
            function (res) {
                console.log(res);
                $("span.upvote-value[data-id=" + id + "]").text("+" + res.upvotes);
            });
    });
var footerBox = document.querySelector('footer');
let observer = new IntersectionObserver(function (items) {
    items.forEach(function(item) {
        if (item.intersectionRatio > 0.5) {
           
            footerBox.classList.remove('translatable-notvisible');
            footerBox.classList.add('translatable-visible');
        } else {
            //footerBox.classList.remove('translatable-visible');
            //footerBox.classList.add('translatable-notvisible');
        }
    });
}, { treshold: [0.5] });

let spy = document.querySelector('.spy-footer');
footerBox.classList.add('translatable-notvisible');
observer.observe(spy);
