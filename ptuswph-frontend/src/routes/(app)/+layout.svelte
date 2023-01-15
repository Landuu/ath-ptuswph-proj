<script>
    import "@/app.postcss";
    import LoginModal from "@/components/LoginModal.svelte";
    import {loggedUser, showLoginModal} from '@/stores';
    import { redirect } from '@sveltejs/kit';
    
    const showLogin = () => {
        showLoginModal.set(true);
    }

    const logout = () => {
        loggedUser.set(null);
        redirect(302, '/');
    }
</script>

<header class="mb-10">
    <div class="navbar">
        <div class="flex items-center">
            <a href="/" class="home-button">
                <i class='bx bx-home'></i>
            </a>
            <div class="text-2xl font-bold text-indigo-400">
                FILMEX
            </div>
        </div>
        <div class="flex items-center">
            {#if $loggedUser}
                <div>
                    <a href="/" class="user-button mr-1">
                        <i class='bx bx-wallet mr-1'></i>
                        100.93 PLN
                    </a>
                    <a href="/" class="user-button mr-4">
                        <i class='bx bxs-user mr-1'></i>
                        {$loggedUser.login}
                    </a>
                    <button class="action-button" on:click={logout}>
                        Wyloguj
                    </button>
                </div>
            {:else}
                <button class="action-button" on:click={showLogin}>
                    Zaloguj się
                </button>
            {/if}
            
        </div>
    </div>
</header>

{#if $showLoginModal}
    <LoginModal />
{/if}

<slot />

<style lang="postcss">
    .navbar {
        @apply w-full flex justify-between;
        @apply text-white px-24 py-2 bg-gray-800 border-b border-gray-700;
    }

    .home-button {
        @apply mr-3 p-2.5 rounded text-lg bg-gray-700 flex items-center shadow;
        @apply hover:bg-gray-600;
    }

    .action-button {
        @apply px-5 py-1.5 bg-indigo-700 shadow;
        @apply hover:bg-indigo-800;
    }

    .user-button {
        @apply px-5 py-1.5 bg-gray-700 shadow;
        @apply hover:bg-gray-600;
    }
</style>
