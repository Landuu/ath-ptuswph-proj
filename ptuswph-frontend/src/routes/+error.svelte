<script lang="ts">
  import { page } from '$app/stores';
</script>
  
<b>ERROR {$page.status}:</b> {$page.error?.message}