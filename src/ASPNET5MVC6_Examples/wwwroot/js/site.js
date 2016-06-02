// site.js

// Javascript en ambito global

//var ele = document.getElementById("username");
//ele.innerHTML = "Eduardo Caceres";

//var main = document.getElementById("main");

//main.onmouseenter = function () {
//    //IE-Firefox
//    main.style = "background-color: #888;";
//    //Chrome
//    main.style["background-color"] = "#888;";

//};

//main.onmouseleave = function () {
//    main.style["background-color"] = "";
//};


(function () {

    //Javascript
    //var ele = document.getElementById("username");
    //ele.innerHTML = "Eduardo Caceres";

    //jQuery -> $ representa el objeto jQuery
    var ele = $("#username");
    ele.text("Eduardo Caceres");

    var main = $("#main");

    //JavaScript eventos
    //main.onmouseenter = function () {
    //    //IE-Firefox
    //    main.style = "background-color: #888;";
    //    //Chrome
    //    main.style["background-color"] = "#888;";
    //};
    main.on("mouseenter", function () {
        //IE-Firefox
        main.style = "background-color: #888;";
        //Chrome
        main.style["background-color"] = "#888;";

    });

    main.on("mouseleave", function () {
        main.style["background-color"] = "";
    });

    var menuItems = $("ul.menu li a");
    menuItems.on("click", function () {
        var me = $(this);
        alert("Hello"+me.text());
    });

}());