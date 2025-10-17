const inputs = document.querySelectorAll(".otp-field input");

inputs[0].focus();

inputs.forEach((input, index) => {
    input.dataset.index = index;
    input.addEventListener("keyup", handleOtp);
    input.addEventListener("paste", handlePaste);
});

function handleOtp(e) {
    const input = e.target;
    const value = input.value.trim();
    const index = Number(input.dataset.index);

    if (!/^[0-9]$/.test(value)) {
        input.value = "";
        return;
    }

    if (index < inputs.length - 1) {
        inputs[index + 1].focus();
    }

    if (index === inputs.length - 1) {
        const allFilled = Array.from(inputs).every(i => i.value.trim() !== "");
        if (allFilled) submit();
    }

    if (e.key === "Backspace" && index > 0) {
        inputs[index - 1].focus();
    }
}

function handlePaste(e) {
    const data = e.clipboardData.getData("text");
    const chars = data.split("");
    if (chars.length === inputs.length) {
        inputs.forEach((input, i) => input.value = chars[i]);
        submit(); 
    }
}

async function submit() {
    console.log("Submitting...");
    let otp = "";
    inputs.forEach(input => {
        otp += input.value;
        input.disabled = true;
        input.classList.add("disabled");
    });

    console.log("OTP:", otp);

    try {
        const response = await fetch('/User/ConfirmPhoneNumber', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ otp })
        });

        if (response.ok) {
            const result = await response.json();
            alert(result.message);
            if (result.success) {
                window.location.href = "/Home/Index";
            }
        } else {
            const error = await response.json();
            alert(error.message || "Invalid OTP");
            inputs.forEach(i => {
                i.disabled = false;
                i.classList.remove("disabled");
                i.value = "";
            });
            inputs[0].focus();
        }
    } catch (err) {
        console.error(err);
        alert("Network error. Please try again.");
        inputs.forEach(i => i.disabled = false);
        inputs[0].focus();
    }
}
