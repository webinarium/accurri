import {createApp} from "vue";
import axios from "axios";

import uid from "@utilities/uid";

console.log("WTF2", uid());

const index = createApp({
    data: () => ({
        todos: []
    }),

    methods: {
        reload() {
            axios.get("/todo")
                .then((response) => this.todos = response.data)
                .catch((error) => console.log(error));
        },

        onAdd() {
            location.assign("/add");
        },

        onEdit(id) {
            location.assign(`/edit/${id}`);
        },

        onDelete(id) {
            axios.delete(`/todo/${id}`)
                .then(() => this.reload())
                .catch((error) => console.log(error));
        }
    },

    created() {
        console.log("wtf", uid());
        this.reload();
    }
});

index.mount("#app");
