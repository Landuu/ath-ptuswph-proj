<script lang="ts">
    import { invalidate } from "$app/navigation";
    import type { ApiTransaction } from "@/types";
    import { getAuthHeaders } from "@/utils";

    export let data: { transactions: ApiTransaction[]};

    const resetWallet = async () => {
        const confirmed = confirm('Czy na pewno chcesz zresetować portfel/transakcje?');
        if(!confirmed) return;

        const res = await fetch('/api/users/wallet/reset', {
            method: 'POST',
            headers: getAuthHeaders()
        })
        
        if(res.status != 200) {
            alert('Błąd podczas resetowania!');
            return;
        }
        invalidate('user:wallet');
        invalidate('user:transactions');
    }
</script>


<div class="px-20">
    <div class="flex justify-between">
        <h1 class="text-2xl font-light">HISTORIA TRANSAKCJI</h1>
        <button class="reset-button" on:click={resetWallet}>Reset portfela i transakcji</button>
    </div>

    <div class="mt-10">
        <table class="w-full text-sm text-left text-gray-500 dark:text-gray-400">
            <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                <tr>
                    <th scope="col">
                        Opis transakcji
                    </th>
                    <th scope="col">
                        Kwota transakcji
                    </th>
                    <th scope="col">
                        Balans po transakcji
                    </th>
                </tr>
            </thead>
            <tbody>
                {#if data.transactions.length == 0}
                    <tr>
                        <th>-</th>
                        <td>-</td>
                        <td>-</td>
                    </tr>
                {/if}
                {#each data.transactions as transaction}
                    <tr>
                        <th scope="row">
                            {transaction.description}
                        </th>
                        <td>
                            <span class="{transaction.ammount > 0 ? 'pos' : 'neg'}">{transaction.ammount > 0 ? "+" + transaction.ammount : transaction.ammount} zł</span> 
                        </td>
                        <td>
                            {transaction.balanceAfter} zł
                        </td>
                    </tr>
                {/each}
            </tbody>
        </table>
    </div>
</div>


<style lang="postcss">
    table > thead > tr > th {
        @apply px-6 py-3;
    }

    table > tbody > tr {
        @apply bg-gray-800;
    }

    table > tbody > tr:not(:last-child) {
        @apply border-b border-gray-700;
    }

    table > tbody > tr > th {
        @apply px-6 py-4 font-medium text-white whitespace-nowrap;
    }

    table > tbody > tr > td {
        @apply px-6 py-4;
    }

    .pos {
        @apply text-green-400;
    }

    .neg {
        @apply text-red-400;
    }

    .reset-button {
        @apply bg-red-700 py-2 px-10 select-none;
        @apply hover:bg-red-800;
    }
</style>