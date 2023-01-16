<script lang="ts">
    import MovieTile from "@/components/MovieTile.svelte";
    import type { ApiMovie } from "@/types";
    export let data: {data: ApiMovie[]};

    const movies = data.data;
</script>


<div class="px-20 mb-20">
    <div class="text-2xl font-light">
        FILMY W NASZEJ BIBLIOTECE
    </div>
    <div class="mt-8 flex flex-wrap">
        {#each movies as movie}
            <MovieTile movie={movie} />
        {/each}
    </div>
</div>

