$(".calculator input").on("input change", function () {
    const self = $(this);
    const parameterName = self.attr("id").split("calc-")[1];
    let centimeters = self.val();

    switch (parameterName) {
        case "height":
            $("#calc-height_value").html("Height: " + centimeters + " cm");
            break;
        case "weight":
            var kg = self.val();
            $("#calc-weight_value").html("Weight: " + kg + " kg");
            break;
        case "age":
            $("#calc-age_value").html("Age: " + self.val());
            break;
        case "cardio":
            $("#calc-cardio_value").html("Cardio: " + self.val() + " hours per week");
            break;
        case "walking":
            $("#calc-walking_value").html("Walking: " + self.val() + " hours per week");
            break;
    }

    let height = parseInt($("#calc-height").val(), 10);
    let age = parseInt($("#calc-age").val(), 10);
    let weight = parseInt($("#calc-weight").val(), 10);
    let walking = parseInt($("#calc-walking").val(), 10);
    let cardio = parseInt($("#calc-cardio").val(), 10);
    let gender = $(".calculator input[name='gender']:checked").val();

    let bmr = parseInt(10 * weight + 6.25 * height - 5 * age, 10) + (gender === "male" ? 5 : -161);
    bmr = bmr * 1.2;
    bmr += walking * 60 * (.03 * weight * 1 / 0.45) / 7;
    bmr += cardio * 60 * (.07 * weight * 1 / 0.45) / 7;
    bmr = Math.floor(bmr);

    let targetGainWeight = Math.round((bmr + 300) / 100) * 100;
    let targetMaintain = Math.round((bmr) / 100) * 100;
    let targetLoseWeight = Math.round((bmr - 500) / 100) * 100;

    $("#calc-target-gain span").html(targetGainWeight + " calories");
    $("#calc-target-maintain span").html(targetMaintain + " calories");
    $("#calc-target-lose span").html(targetLoseWeight + " calories");
});