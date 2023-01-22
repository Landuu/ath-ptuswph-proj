<script lang="ts">
    import { goto, invalidate } from "$app/navigation";
    import { loggedUser } from "@/stores";
    import type { LoggedUser } from "@/types";
    import { getAuthToken } from "@/utils";
    import store from "store2";


    let inputLogin: string;
    let inputPassword: string;

    const registerUser = async () => {
        if(!inputLogin || !inputPassword) return;
        if(inputLogin.length < 2 || inputPassword.length < 2) return;

        inputLogin = inputLogin.trim();
        inputPassword = inputPassword.trim();

        const payload = {
            Login: inputLogin,
            Password: inputPassword
        }

        const res = await fetch('/api/auth/register', {
            method: 'POST',
            headers: {
                'Authorization': getAuthToken(),
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(payload)
        });
        if(!res.ok) {
            alert('Nieprawidłowy login lub hasło. (min 2 znaki, login musi być unikalny)');
            return;
        }

        const user: LoggedUser = await res.json();
        loggedUser.set(user);
        store.session.set('loggedUser', user);
        invalidate('user:wallet');
        invalidate('register');
    }
</script>

<div class="px-20">
    <h1>REJESTRACJA W SERWISIE FILMEX</h1>

    <div class="mt-10 mb-16 flex flex-col justify-center">
        <div class="mx-auto flex flex-col">
            <label for="modal-login">Login:</label>
            <input id="modal-login" bind:value={inputLogin} />
        </div>
        <div class="mx-auto flex flex-col mt-3">
            <label for="modal-password">Hasło:</label>
            <input
                id="modal-password"
                type="password"
                bind:value={inputPassword}
            />
        </div>

        <div class="text-center mt-4">
            <button on:click={registerUser}>Zarejestruj</button>
        </div>
    </div>
</div>

<style lang="postcss">



    h1 {
        @apply text-2xl font-light;
    }

    input {
        @apply w-96 mx-auto mt-3 mb-5 bg-gray-700 p-2;
    }

    button {
        @apply bg-indigo-700 py-2 px-10 select-none w-auto text-lg;
        @apply hover:bg-indigo-800;
    }
</style>
