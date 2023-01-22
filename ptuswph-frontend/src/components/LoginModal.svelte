<script lang="ts">
    import { invalidate } from "$app/navigation";
    import { showLoginModal, loggedUser } from "@/stores";
    import type { LoggedUser } from "@/types";
    import { getAuthToken } from "@/utils";
    import { redirect } from "@sveltejs/kit";
    import store from "store2";

    let inputLogin: string;
    let inputPassword: string;

    const closeModal = () => {
        showLoginModal.set(false);
    }

    const sendCredentials = async () => {
        if(!inputLogin || !inputPassword) return;
        inputLogin = inputLogin.trim();
        inputPassword = inputPassword.trim();
        if(inputLogin.length == 0 || inputPassword.length == 0) return;

        const payload = {
            Login: inputLogin,
            Password: inputPassword
        };
        const res = await fetch('/api/auth', {
            method: 'POST',
            headers: {
                'Authorization': getAuthToken(),
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(payload)
        });
        if(res.status != 200) {
            alert("Nieprawidłowy login lub hasło!");
            return;
        }

        const userdata: LoggedUser = await res.json();
        store.session.set('loggedUser', userdata);
        loggedUser.set(userdata);
        invalidate('user:wallet');
        invalidate('register');
        closeModal();
    }
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->
<div class="backdrop" on:click={closeModal}></div>
<div class="modal">
    <div class="flex justify-between text-lg select-none">
        <div class="py-2">LOGOWANIE</div>
        <button class="px-3 py-1 hover:cursor-pointer flex items-center" on:click={closeModal}>
            X
        </button>
    </div>
    <div class="mt-6 mb-2 form">
        <label for="modal-login">Login</label>
        <input class="mb-4" id="modal-login" bind:value={inputLogin} />

        <label for="modal-password">Hasło</label>
        <input id="modal-password" type="password" bind:value={inputPassword} />

        <div class="w-full flex justify-center mt-8">
            <button class="login-button" on:click={sendCredentials}>Zaloguj</button>
        </div>
    </div>
</div>


<style lang="postcss">
    .backdrop {
        @apply w-full h-full fixed top-0 left-0 bg-black opacity-70 z-10;
    }

    .modal {
        @apply w-96 h-auto bg-gray-800 z-20 absolute p-8;
        left: 50%;
        top: 50%;
        transform: translate(-50%, -50%);
    }

    .form > input {
        @apply w-full bg-gray-700 p-2;
    }

    .form > label {
        @apply text-gray-300 select-none;
    }

    .login-button {
        @apply bg-indigo-700 py-2 px-10 select-none;
        @apply hover:bg-indigo-800;
    }
</style>