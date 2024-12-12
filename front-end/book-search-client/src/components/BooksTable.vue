<template>
    <div>
        <input v-model="filterText" placeholder="Filter by title or author" />
        <table>
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Author</th>
                    <th>Year</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="book in filteredBooks" :key="book.key">
                    <td>{{ book.title }}</td>
                    <td>{{ book.authorName }}</td>
                    <td>{{ book.firstPublishYear }}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, onMounted } from 'vue'

interface BookDto {
    title?: string;
    authorName?: string;
    firstPublishYear?: number;
}

export default defineComponent({
    setup() {
        const books = ref<BookDto[]>([]);
        const filterText = ref('');

        const filteredBooks = computed(() => {
            const ft = filterText.value.toLowerCase();
            return books.value.filter(b => {
                const titleMatch = b.title?.toLowerCase().includes(ft) ?? false;
                const authorMatch = b.authorName?.toLowerCase().includes(ft) ?? false;
                return titleMatch || authorMatch;
            });
        });

        onMounted(async () => {
            const res = await fetch('https://localhost:7269/Books');
            books.value = await res.json();
        });

        return { books, filterText, filteredBooks };
    }
})
</script>

<style scoped>
table {
    width: 100%;
    border-collapse: collapse;
}

th,
td {
    border: 1px solid #ccc;
    padding: 8px;
}

th {
    background: #eee;
}
</style>
