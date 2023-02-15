const frm = document.querySelector("form");

frm.addEventListener("submit", function (e) {
	e.preventDefault();
	e.stopPropagation();

	frm.classList.add('was-validated');

	const tb = document.querySelector("#inputlink");

	if (frm.checkValidity()) { // the form is valid

		fetch("/api/short-link", {
			method: "POST",
			headers: {
				"Content-Type": "application/json;"
			},
			body: JSON.stringify(tb.value)
		})
			.then(response => response.json())
			.then(data => {
				document.querySelector("#result").value = data.short;
			});
	}
});

