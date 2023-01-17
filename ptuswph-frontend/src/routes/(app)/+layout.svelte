<script lang="ts">
    import "@/app.postcss";
    import Icon from "@/components/Icon.svelte";
    import LoginModal from "@/components/LoginModal.svelte";
    import UserNav from "@/components/UserNav.svelte";
    import { loggedUser, showLoginModal } from '@/stores';
    
    const showLogin = () => {
        showLoginModal.set(true);
    }
</script>

<header class="mb-10">
    <div class="navbar">
        <div class="flex items-center">
            <a href="/" class="home-button">
                <Icon className="bi-house" />
            </a>
            <div class="text-2xl font-bold text-indigo-400">
                FILMEX
            </div>
        </div>
        <div class="flex items-center">
            {#if $loggedUser}
                <div class="flex">
                    <UserNav />
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
        @apply mr-3 py-2 px-3 text-xl bg-gray-700 flex items-center shadow;
        @apply hover:bg-gray-600;
    }

    .action-button {
        @apply px-5 py-1.5 bg-indigo-700 shadow;
        @apply hover:bg-indigo-800;
    }
</style>
