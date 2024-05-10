/**
 * main.ts
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Plugins
import { registerPlugins } from "@/plugins";

// Components
import App from "./App.vue";

// Composables
import { createApp } from "vue";
import { errorHandler } from "./config";

const app = createApp(App);
app.config.errorHandler = errorHandler;
registerPlugins(app);

app.mount("#app");
