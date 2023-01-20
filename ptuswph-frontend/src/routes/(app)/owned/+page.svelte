
<script lang="ts">
    import { invalidate } from "$app/navigation";
    import type { ApiMovie } from "@/types";
    import { getAuthHeaders, getAuthOptions, getAuthToken } from "@/utils";
    import { saveAs } from 'file-saver';

    export let data: { movies: ApiMovie[]};

    const resetOwned = async () => {
        const confirmed = confirm('Czy na pewno chcesz zresetować posiadane filmy?');
        if(!confirmed) return;

        const res = await fetch('/api/users/movies/reset', {
            method: 'DELETE',
            headers: getAuthHeaders()
        });
        if(!res.ok) {
            alert('Błąd resetowania filmów');
            console.log(res);
            return;
        }

        invalidate('user:owned');
    }

    const test = async () => {
        const res = await fetch('/api/movies/125/download', {
            method: 'GET',
            headers: getAuthHeaders()
        });
        console.log(res);
        return;
    }

    const downloadMovie = async (movie: ApiMovie) => {

        const url = `/api/movies/${movie.id}/download`;
        const res = await fetch(url, getAuthOptions());
        console.log('response', res);
        if(!res.ok) {
            alert('Błąd pobierania filmu');
            console.log(res);
            return;
        }
        const file = await res.blob();
        const fileName = movie.title + '123.jpg';
        saveAs(file, fileName);
    }
</script>

<div class="px-20 mb-20">
    <div class="flex justify-between">
        <h1 class="text-2xl font-light">POSIADANE FILMY</h1>
        <button class="reset-button" on:click={resetOwned}>Reset posiadanych filmów</button>
    </div>

    <div class="mt-10">
        <table class="w-full text-sm text-left text-gray-500 dark:text-gray-400">
            <thead class="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400">
                <tr>
                    <th scope="col">
                        Nazwa filmu
                    </th>
                    <th scope="col">
                        Data wydania
                    </th>
                    <th scope="col">
                        Dostęp
                    </th>
                </tr>
            </thead>
            <tbody>
                {#if data.movies.length == 0}
                    <tr>
                        <th>-</th>
                        <td>-</td>
                        <td>-</td>
                    </tr>
                {/if}
                {#each data.movies as movie}
                    <tr>
                        <th scope="row">
                            {movie.title}
                        </th>
                        <td>
                            {movie.release}
                        </td>
                        <td>
                            <button class='download-button' on:click={() => downloadMovie(movie)}>
                                Pobierz
                            </button>
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
        @apply px-6 py-3;
    }

    .download-button {
        @apply bg-indigo-700 py-2 px-8 select-none text-white;
        @apply hover:bg-indigo-800;
    }

    .reset-button {
        @apply bg-red-700 py-2 px-10 select-none;
        @apply hover:bg-red-800;
    }
</style>