<script lang="ts">
    import type { ApiMovie } from "@/types";
    import { redirect } from "@sveltejs/kit";


    export let movie: ApiMovie;
    const year = movie.release.slice(-4);

    const movieRedirect = (id: number) => {
        redirect(302, `/movies/${id}`);
    }
</script>

<a class="w-52 hover:bg-gray-700 hover:cursor-pointer p-3 mx-auto mt-5" href="/movies/{movie.id}">
    <img class="w-full h-auto" src="assets/{movie.img}" alt="{movie.title}" />
    <div class="mt-2">({year}) {movie.title}</div>
</a>