// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var listCliked = [];


function displayComments(tweet) {

    var comment = document.getElementById("Tweet_" + tweet);

    if (listCliked.includes(tweet)) {
        comment.classList.add("d-none");
        listCliked.pop(tweet);
    } else {

        comment.classList.remove("d-none");
        listCliked.push(tweet);
    }

}

