let ctxL = document.getElementById("lineChart").getContext('2d');
let ctxt = document.getElementById("RecipesnArticlesChar").getContext('2d');

let myLineChart = new Chart(ctxL, {
    type: 'line',
    data: {
        labels: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
        datasets: [{
            label: "Average Daily Users",
            data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            borderColor: [
                'rgba(200, 99, 132, .7)',
            ],
            borderWidth: 3
        },
        {
            label: "Registered Users",
            data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            borderColor: [
                'rgba(0, 10, 130, .7)',
            ],
            borderWidth: 3
        }
        ]
    },
    options: {
        responsive: true
    }
});

let myDoughnutChart = new Chart(ctxt, {
    type: 'pie',
    data: {
        labels: ["Articles", "Recipes", "Reviews"],
        datasets: [{

            data: [0, 0, 0, 0],
            backgroundColor: ["#3e95cd", "#c45850", "#52BE80", "#F7DC6F"],
        }, {
            data: [0, 0, 0, 0],
            backgroundColor: ["#3e95cd", "#c45850", "#52BE80", "#F7DC6F"],
        }]

    },
    options: {
        responsive: false,
        legend: {
            position: "right",
            align: "middle"
        },
    }
});
$(document).ready(function () {
    $.ajax({
        type: 'GET',
        data: { type: "Categories" },
        url: '/Administration/Dashboard/FetchTopFive',
        success: function (data) {
            $('#topCategories').append(`<div class="br-b-white p-3">Top Categories</div>`);
            data.forEach(el => $('#topCategories').append(`<div class="pt-3 sm">` + el.key + " - " + el.count + ` Recipes</div>`));
        }
    });
    $.ajax({
        type: 'GET',
        data: { type: "Recipes" },
        url: '/Administration/Dashboard/FetchTopFive',
        success: function (data) {
            $('#topRecipes').append(`<div class="br-b-white p-3">Top Recipes</div>`);
            data.forEach(el => $('#topRecipes').append(`<div class="pt-3 sm">` + el.key + " - " + el.rate + ` <i class="fas fa-star star"></i></div>`));
        }
    });
});