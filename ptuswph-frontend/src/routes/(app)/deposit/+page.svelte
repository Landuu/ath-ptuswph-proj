<script lang="ts">
    import { invalidate } from "$app/navigation";
    import DepositMethod from "@/components/DepositMethod.svelte";
    import { getAuthToken } from "@/utils";

    let inputAmmount: number;
    let loading = false;

    const depositMoney = async () => {
        if(!inputAmmount) return;
        inputAmmount = Number(inputAmmount.toFixed(2));
        loading = true;

        setTimeout(async () => {
            const payload = {
                Ammount: inputAmmount
            }
            const res = await fetch('/api/users/wallet', {
                method: 'POST',
                headers: {
                    'Authorization': getAuthToken(),
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(payload)
            })
            
            if(res.status != 200) {
                alert('Błąd podczas wpłacania środków!');
                return;
            }
            invalidate('user:wallet');
            loading = false;
            location.href = '/';
        }, 1500);
    }
</script>

<div class="px-20">
    <h1>WPŁAĆ ŚRODKI DO PORTFELA</h1>

    <div class="mt-10 flex justify-center flex-grow-0 flex-wrap">
        <DepositMethod name='Mastercard' img='/assets/img/mastercard.png' />
        <DepositMethod name='Visa' img='/assets/img/visa.png' />
        <DepositMethod name='BLIK' img='/assets/img/blik.png' />
        <DepositMethod name='Wirtualny portfel' disabled={false} />
    </div>

    <div class="mt-10 mb-16 flex flex-col justify-center">
        <h2>Kwota:</h2>
        <input type='number' bind:value={inputAmmount} />
        <div class="text-center">
            <button on:click={depositMoney} disabled={loading}>{!loading ? 'Wpłać' : '...'}</button>
        </div>
    </div>
    
</div>

<style lang="postcss">
    h1 {
        @apply text-2xl font-light;
    }

    h2 {
        @apply mx-auto text-2xl;
    }

    input {
        @apply w-64 mx-auto mt-3 mb-5 bg-gray-700 p-2 text-center;
    }

    button {
        @apply bg-indigo-700 py-2 px-10 select-none w-auto text-lg;
        @apply hover:bg-indigo-800;
    }

    button:disabled {
        @apply bg-gray-500 hover:cursor-progress;
    }
</style>