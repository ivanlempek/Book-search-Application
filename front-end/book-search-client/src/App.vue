<template>
  <BooksTable />
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import BooksTable from './components/BooksTable.vue'

export default defineComponent({
  components: { BooksTable }
})
</script>
