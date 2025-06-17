document.querySelectorAll('input[type="password"]').forEach(function (input) {
        if (input.parentElement.querySelector('.toggle-password')) return; // para hindi mag-duplicate

        const wrapper = document.createElement('div');
        wrapper.style.position = 'relative';

        const eyeIcon = document.createElement('span');
        eyeIcon.innerHTML = `
      <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor"
        class="bi bi-eye-fill toggle-password" viewBox="0 0 16 16">
        <path d="M10.5 8a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0"/>
        <path d="M0 8s3-5.5 8-5.5S16 8 16 8s-3 5.5-8 5.5S0 8 0 8m8 3.5a3.5 
        3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7"/>
      </svg>
    `;
        eyeIcon.style.position = 'absolute';
        eyeIcon.style.right = '10px';
        eyeIcon.style.top = '38%';
        eyeIcon.style.transform = 'translateY(-50%)';
        eyeIcon.style.cursor = 'pointer';

        input.parentNode.insertBefore(wrapper, input);
        wrapper.appendChild(input);
        wrapper.appendChild(eyeIcon);

        eyeIcon.addEventListener('click', function () {
            if (input.type === 'password') {
                input.type = 'text';
                eyeIcon.innerHTML = `
              <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor"
                class="bi bi-eye-slash-fill toggle-password" viewBox="0 0 16 16">
                <path d="m10.79 12.912-1.614-1.615a3.5 3.5 
                0 0 1-4.474-4.474l-2.06-2.06C.938 6.278 
                0 8 0 8s3 5.5 8 5.5a7 7 0 0 0 
                2.79-.588M5.21 3.088A7 7 0 0 1 8 
                2.5c5 0 8 5.5 8 5.5s-.939 1.721-2.641 
                3.238l-2.062-2.062a3.5 3.5 0 0 0-4.474-4.474z"/>
                <path d="M5.525 7.646a2.5 2.5 
                0 0 0 2.829 2.829zm4.95.708-2.829-2.83a2.5 
                2.5 0 0 1 2.829 2.829zm3.171 6-12-12 
                .708-.708 12 12z"/>
              </svg>
            `;
            } else {
                input.type = 'password';
                eyeIcon.innerHTML = `
              <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor"
                class="bi bi-eye-fill toggle-password" viewBox="0 0 16 16">
                <path d="M10.5 8a2.5 2.5 
                0 1 1-5 0 2.5 2.5 0 0 1 5 0"/>
                <path d="M0 8s3-5.5 8-5.5S16 
                8 16 8s-3 5.5-8 5.5S0 8 0 
                8m8 3.5a3.5 3.5 0 1 0 0-7 
                3.5 3.5 0 0 0 0 7"/>
              </svg>
            `;
            }
        });
    });

function loadAdminList() {
    
}

<script>
    document.getElementById("addAdminForm").addEventListener("submit", function (e) {
        e.preventDefault(); // prevent page refresh

    const name = document.getElementById("adminName").value;
    const address = document.getElementById("adminAddress").value;
    const email = document.getElementById("adminEmail").value;
    const number = document.getElementById("adminNumber").value;
    const position = document.getElementById("adminPosition").value;
    const password = document.getElementById("adminPassword").value;

    console.log("Admin Added:", {name, address, email, number, position, password});

    alert("Admin Added:\n" +
    "Name: " + name + "\n" +
    "Address: " + address + "\n" +
    "Email: " + email + "\n" +
    "Number: " + number + "\n" +
    "Position: " + position + "\n" +
    "Password: " + password);

    document.getElementById("addAdminForm").reset();
    });
</script>

