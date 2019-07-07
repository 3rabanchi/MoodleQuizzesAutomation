$(document).ready(function () {
    var i = 2;
        

    $("button").click(function () {
        i++;
        $("#inputs").append(
            "<tr>" +
            "<td><input class=\"active\" id = " + i + "1></td>" +
            "<td><input class=\"active\" id = " + i + "2></td>" +
            "<td><input class=\"active\" id = " + i + "3></td>" +
            "</tr>"
        );
    });

    $('#Test').submit(function (e) {
        e.preventDefault();
        var x = 0;
        var temp1 = i + 1;
        var mainArray = new Array();
        while (x <= i) {
            var first = "[id=\"" + x + "1\"]";
            var second = "[id=\"" + x + "2\"]";
            var third = "[id=\"" + x + "3\"]";
            var temp = {
                "firstcell": document.querySelector(first).value,
                "secondCell":document.querySelector(second).value,
                "thirdCell":document.querySelector(third).value
            }
          //  temp = JSON.stringify(temp);
            mainArray.push(temp);
            x++;
        }

        var asd = document.querySelector('[id="03"]').value;
        //    var asd = mainArray[0][0];
       // mainArray = convertToJSON(mainArray);
        console.log(JSON.stringify(mainArray));
        $.ajax({
            type: "POST",
            url: "/Questions/X86",
            data: {
                cellValues: JSON.stringify(mainArray),
                title: JSON.stringify(document.querySelector('[id=questionTitle]').value)
            },
         //   contentType: "application/json",
            dataType: 'json',
            success: function (result) {
                console.log('Data received: ');
                console.log(result);
                window.location.replace("Questions");

            },
            error: function (result) {
                window.location.replace("Questions")
            }
        });
    })

});