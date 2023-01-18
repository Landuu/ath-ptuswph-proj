<script lang="ts">
    import Icon from "./Icon.svelte";

    export let name: string;
    export let img: string | null = null;
    export let disabled: boolean = true;
</script>


<div class="container">
    <div class="method" data-disabled={disabled}>
        <div class="w-full h-full flex justify-center">
            {#if img}
                <img src={img} alt={name} />
            {/if}
            {#if name == 'Wirtualny portfel'}
                <Icon className="bi-currency-exchange text-5xl my-auto" />
            {/if}
        </div>
        <div class="w-full h-auto text-center">
            {name}
        </div>
    </div>
    <div class="text-center font-light text-gray-400">
        {#if disabled} Niedostępne {/if}
    </div>
</div>


<style lang="postcss">
    .container {
        @apply w-auto mx-5 my-3
    }

    .method {
        @apply bg-gray-700 w-64 h-36 justify-center items-center shadow flex flex-col p-5;
    }

    .method:not([data-disabled='true']) {
        @apply hover:cursor-pointer;
    }

    .method[data-disabled='true'] {
        @apply opacity-40 hover:cursor-not-allowed;
    }

    img {
        @apply w-auto h-full p-6
    }
</style>