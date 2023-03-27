function setInputFilter(textbox, inputFilter, errMsg) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop", "focusout"].forEach(function (event) {
        textbox.addEventListener(event, function (e) {
            if (inputFilter(this.value)) {
                // Accepted value.
                if (["keydown", "mousedown", "focusout"].indexOf(e.type) >= 0) {
                    this.classList.remove("input-error");
                    this.setCustomValidity("");
                }

                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            }
            else if (this.hasOwnProperty("oldValue")) {
                // Rejected value: restore the previous one.
                this.classList.add("input-error");
                this.setCustomValidity(errMsg);
                this.reportValidity();
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            }
            else {
                // Rejected value: nothing to restore.
                this.value = "";
            }
        });
    });
}





setInputFilter(document.getElementById("myTextBox"), function (value) {
    return /^\d*\.?\d*$/.test(value); // Allow digits and '.' only, using a RegExp.
}, "Only digits and '.' are allowed");
setInputFilter(document.getElementById("myTextBox1"), function (value) {
    return /^\d*\.?\d*$/.test(value); // Allow digits and '.' only, using a RegExp.
}, "Only digits and '.' are allowed");
setInputFilter(document.getElementById("myTextBox2"), function (value) {
    return /^\d*\.?\d*$/.test(value); // Allow digits and '.' only, using a RegExp.
}, "Only digits and '.' are allowed");
setInputFilter(document.getElementById("myTextBox3"), function (value) {
    return /^\d*\.?\d*$/.test(value); // Allow digits and '.' only, using a RegExp.
}, "Only digits and '.' are allowed");


function changeOp(sel) {
    var text2 = sel.options[sel.selectedIndex].value;
    $.ajax({
        url: 'test5/Home/Calc2',
        dataType: 'html',
        data: { CountryOut: text2 },
        success: function (data) {
            $('#сitypanel2').html(data);
        }
    });
}

function changeOption(sel) {
    var text = sel.options[sel.selectedIndex].value;
    $.ajax({
        url:'test5/Home/Calc1',
        dataType: 'html',
        data: { CountryOut: text},
        success: function (data) {
            $('#сitypanel').html(data);
        }
    });
} 
function Clear1(sel)
{
    if (sel.options[sel.selectedIndex].value == 1)
    {
        var myNode = document.getElementById("Innreg");
        while (myNode.firstChild) {
            myNode.removeChild(myNode.firstChild);
        }
    }
    else
    {
        var text = sel.options[sel.selectedIndex].value;
        $.ajax({
            url: 'http://95.163.88.13/test5/home/GerOr',
            dataType: 'html',
            data: { num: text },
            success: function (data) {
                $('#Innreg').html(data);
            }
        });
    }
}

function ClearOrder2(sel) {
    if (sel.options[sel.selectedIndex].value == 1) {
        var myNode = document.getElementById("InnSender");
        while (myNode.firstChild) {
            myNode.removeChild(myNode.firstChild);
        }
    }
    else {
        var text = sel.options[sel.selectedIndex].value;
        $.ajax({
            url: 'http://95.163.88.13/test5/home/OrdOr2',
            dataType: 'html',
            data: { num: text },
            success: function (data) {
                $('#InnSender').html(data);
            }
        });
    }
}



function ClearOrder1(sel) {
    if (sel.options[sel.selectedIndex].value == 1) {
        var myNode = document.getElementById("InnRecipient");
        while (myNode.firstChild) {
            myNode.removeChild(myNode.firstChild);
        }
    }
    else {
        var text = sel.options[sel.selectedIndex].value;
        $.ajax({
            url: 'http://95.163.88.13/test5/home/OrdOr',
            dataType: 'html',
            data: { num: text },
            success: function (data) {
                $('#InnRecipient').html(data);
            }
        });
    }
}

Highcharts.chart('container', {
    chart: {
        type: 'column'
    },
    title: {
        text: 'Browser market shares. January, 2015 to May, 2015'
    },
    subtitle: {
        text: 'Click the columns to view versions. Source: netmarketshare.com.'
    },
    xAxis: {
        type: 'category'
    },
    yAxis: {
        title: {
            text: 'Total percent market share'
        }
    },
    legend: {
        enabled: false
    },
    plotOptions: {
        series: {
            borderWidth: 0,
            dataLabels: {
                enabled: true,
                format: '{point.y:.1f}%'
            }
        }
    },
    tooltip: {
        headerFormat: '{series.name}',
        pointFormat: '{point.name}: {point.y:.2f}% of total'
    },
    series: [{
        name: 'Brands',
        colorByPoint: true,
        data: [{
            name: 'Microsoft Internet Explorer',
            y: 56.33,
            drilldown: 'Microsoft Internet Explorer'
        }, {
            name: 'Chrome',
            y: 24.03,
            drilldown: 'Chrome'
        }, {
            name: 'Firefox',
            y: 10.38,
            drilldown: 'Firefox'
        }, {
            name: 'Safari',
            y: 4.77,
            drilldown: 'Safari'
        }, {
            name: 'Opera',
            y: 0.91,
            drilldown: 'Opera'
        }, {
            name: 'Proprietary or Undetectable',
            y: 0.2,
            drilldown: null
        }]
    }],
    drilldown: {
        series: [{
            name: 'Microsoft Internet Explorer',
            id: 'Microsoft Internet Explorer',
            data: [
                [
                    'v11.0',
                    24.13
                ],
                [
                    'v8.0',
                    17.2
                ],
                [
                    'v9.0',
                    8.11
                ],
                [
                    'v10.0',
                    5.33
                ],
                [
                    'v6.0',
                    1.06
                ],
                [
                    'v7.0',
                    0.5
                ]
            ]
        }, {
            name: 'Chrome',
            id: 'Chrome',
            data: [
                [
                    'v40.0',
                    5
                ],
                [
                    'v41.0',
                    4.32
                ],
                [
                    'v42.0',
                    3.68
                ],
                [
                    'v39.0',
                    2.96
                ],
                [
                    'v36.0',
                    2.53
                ],
                [
                    'v43.0',
                    1.45
                ],
                [
                    'v31.0',
                    1.24
                ],
                [
                    'v35.0',
                    0.85
                ],
                [
                    'v38.0',
                    0.6
                ],
                [
                    'v32.0',
                    0.55
                ],
                [
                    'v37.0',
                    0.38
                ],
                [
                    'v33.0',
                    0.19
                ],
                [
                    'v34.0',
                    0.14
                ],
                [
                    'v30.0',
                    0.14
                ]
            ]
        }, {
            name: 'Firefox',
            id: 'Firefox',
            data: [
                [
                    'v35',
                    2.76
                ],
                [
                    'v36',
                    2.32
                ],
                [
                    'v37',
                    2.31
                ],
                [
                    'v34',
                    1.27
                ],
                [
                    'v38',
                    1.02
                ],
                [
                    'v31',
                    0.33
                ],
                [
                    'v33',
                    0.22
                ],
                [
                    'v32',
                    0.15
                ]
            ]
        }, {
            name: 'Safari',
            id: 'Safari',
            data: [
                [
                    'v8.0',
                    2.56
                ],
                [
                    'v7.1',
                    0.77
                ],
                [
                    'v5.1',
                    0.42
                ],
                [
                    'v5.0',
                    0.3
                ],
                [
                    'v6.1',
                    0.29
                ],
                [
                    'v7.0',
                    0.26
                ],
                [
                    'v6.2',
                    0.17
                ]
            ]
        }, {
            name: 'Opera',
            id: 'Opera',
            data: [
                [
                    'v12.x',
                    0.34
                ],
                [
                    'v28',
                    0.24
                ],
                [
                    'v27',
                    0.17
                ],
                [
                    'v29',
                    0.16
                ]
            ]
        }]
    }
});













