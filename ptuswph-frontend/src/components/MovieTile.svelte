<script lang="ts">
    import type { ApiMovie } from "@/types";


    export let movie: ApiMovie;
    const year = movie.release.slice(-4);
</script>

<div class="w-52 hover:bg-gray-700 hover:cursor-pointer p-3 mx-auto mt-5">
    <img class="w-full h-auto" src="assets/{movie.img}" alt="{movie.title}" />
    <div class="mt-2">({year}) {movie.title}</div>
</div>