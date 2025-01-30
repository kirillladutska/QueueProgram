let submitIsClicked = false;

document.addEventListener('DOMContentLoaded', function() {
    document.getElementById("submit-button").addEventListener("click", async function () {
        if (submitIsClicked) {
            return;
        }

        submitIsClicked = true;
        const errorBlock = document.getElementById("error-block");

        var sel = document.getElementById("visit-option");
        var text = sel.options[sel.selectedIndex].text;

        let formData = {
            selectedOption: text,
            email: document.getElementById("email").value,
            phone: document.getElementById("phone").value
        };

        console.log('FormData:', formData);

        try {
            const response = await fetch('/homesubmit', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(formData)
            });

            console.log('Response status:', response.status);

            if (!response.ok) {
                const errorData = await response.json();
                console.log('Error data:', errorData);
                errorBlock.textContent = errorData.errorMessage;
                errorBlock.style.display = "block";
                submitIsClicked = false;
            } else {
                const visitResponse = await response.json();
                localStorage.setItem('visitInfo', JSON.stringify(visitResponse));
                window.location.href = "livequeue";
            }
        } catch (error) {
            errorBlock.style.display = "block";
            submitIsClicked = false;
        }
    });
});