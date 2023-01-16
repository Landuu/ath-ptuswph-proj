<script lang="ts">
    import Icon from "@/components/Icon.svelte";
    import { loggedUser, loggedUserBalance } from "@/stores";
    import type { ApiMovie } from "@/types";

    export let data: { movie: ApiMovie };

    let canBuy = !($loggedUserBalance != null && $loggedUserBalance - data.movie.price >= 0);
</script>


<div class="px-20">
    <div class="flex justify-between">
        <div>
            <div class="text-3xl">{data.movie.title}</div>
            <div>{data.movie.release}</div>
        </div>
        <div>
            <div class="flex">
                <div class="pricetag">
                    {data.movie.price}zł
                </div>
                {#if $loggedUser}
                    <button class='buy-button' disabled={canBuy}>
                        <Icon className="bi-cart mr-1"/>
                        Wykup dostęp
                    </button>
                {:else}
                    <button class='buy-button' disabled>
                        <Icon className="bi-cart mr-1" />
                        Zaloguj się, aby kupić
                    </button>
                {/if}
                
            </div>
        </div>
    </div>
    
    <div class="mt-8 flex">
        <div class="w-auto">
            <img class="cover" src="/assets/{data.movie.img}" alt={data.movie.title} />
        </div>
        <div class="ml-10 text-xl w-full">
            <div>
                <span class="mr-3 font-bold">Kategoria:</span>
                {data.movie.category}
            </div>
            <div class="mt-8">
                {data.movie.description}
            </div>
        </div>
        
    </div>
</div>

<style lang="postcss">
    .buy-button {
        @apply py-2 px-5 bg-indigo-700 shadow hover:bg-indigo-600;
    }

    .buy-button:disabled {
        @apply bg-gray-500;
    }

    .pricetag {
        @apply py-2 px-5 bg-gray-700 shadow;
    }

    .cover {
        @apply w-96 h-auto shadow-lg border border-gray-700;
    }
</style>