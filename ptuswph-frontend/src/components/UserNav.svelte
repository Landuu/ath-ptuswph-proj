<script lang="ts">
    import { loggedUser, loggedUserBalance } from "@/stores";
    import Icon from "./Icon.svelte";
</script>

<a href="/" class="user-button mr-1">
    <Icon className="bi-wallet2 mr-1" />
    {$loggedUserBalance} zł
</a>
<a href="/" class="user-button mr-4">
    <Icon className="bi-person-fill mr-1" />
    {$loggedUser?.login}
</a>

<style lang="postcss">
    .user-button {
        @apply px-5 py-1.5 bg-gray-700 shadow;
        @apply hover:bg-gray-600;
    }
</style>