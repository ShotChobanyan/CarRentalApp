let form = document.forms['rentDates'];
form.onsubmit = function (e) {
    e.preventDefault();
    let fromDate = new Date(document.form.From.value);
    let toDate = new Date(document.form.To.value);
    let dayCount = toDate.getDate() - fromDate.getDate();
    let TotalPrice = dayCount 
}
