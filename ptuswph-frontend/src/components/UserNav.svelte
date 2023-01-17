<script lang="ts">
    import { loggedUser, loggedUserBalance } from "@/stores";
    import store from "store2";
    import Icon from "./Icon.svelte";

    let walletDropdown = false;
    let userDropdown = false;

    const closeOutside = (e: any) => {
        if(walletDropdown && !e.target.matches('#wallet-drop'))
            walletDropdown = !walletDropdown;

        if(userDropdown && !e.target.matches('#user-drop'))
            userDropdown = !userDropdown;
    }
    
    const logout = () => {
        store.session.remove('loggedUser');
        loggedUser.set(null);
    }
</script>

<svelte:window on:click={(e) => closeOutside(e)} />
<div>
    <button id="wallet-drop" class="user-button mr-1" on:click={() => walletDropdown = !walletDropdown}>
        <Icon className="bi-wallet2 mr-1" />
        {$loggedUserBalance?.toFixed(2)} zł
    </button>
    <div class="dropdown-content {walletDropdown ? "show" : ""}">
        <a class="drop-item" href="#">
            <Icon className="bi-plus-circle mr-2" />
            Wpłać
        </a>
        <a class="drop-item" href="#">
            <Icon className="bi-list-columns-reverse mr-2" />
            Historia transakcji
        </a>
    </div>
</div>
<div>
    <button id="user-drop" class="user-button mr-4" on:click={() => userDropdown = !userDropdown}>
        <Icon className="bi-person-fill mr-1" />
        {$loggedUser?.login}
    </button>
    <div class="dropdown-content {userDropdown ? "show" : ""}">
        <a class="drop-item" href="#">
            <Icon className="bi-collection mr-2" />
            Twoje filmy
        </a>
        <button class="drop-item" on:click={logout}>
            <Icon className="bi-box-arrow-left mr-2" />
            Wyloguj
        </button>
    </div>
</div>
    


<style lang="postcss">
    .user-button {
        @apply px-5 py-1.5 bg-gray-700 shadow;
        @apply hover:bg-gray-600;
    }

    .dropdown-content {
        @apply hidden absolute bg-gray-700 shadow z-[1];
        min-width: 160px;
    }

    .dropdown-content a, .dropdown-content button {
        @apply px-5 py-2 block select-none;
        @apply hover:bg-gray-900;
        text-decoration: none;
    }
    .dropdown-content button {
        @apply w-full text-left;
    }

    .show {
        @apply block;
    }
</style>