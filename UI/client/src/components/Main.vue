<template>
  <v-container>
    <v-row class="top-panel">
      <v-col>
        <v-select
          v-model="filters.fromYear"
          label="С"
          :items="years"
          density="compact"
          variant="solo"
        ></v-select>
      </v-col>
      <v-col>
        <v-select
          v-model="filters.toYear"
          label="По"
          :items="years"
          density="compact"
          variant="solo"
        ></v-select>
      </v-col>
      <v-col>
        <v-text-field
          v-model="filters.meteoriteName"
          label="Название метеорита"
          density="compact"
          variant="solo"
        ></v-text-field>
      </v-col>
      <v-col>
        <v-select
          v-model="filters.meteoriteClass"
          label="Класс метеорита"
          :items="allMeteoritesClasses"
          density="compact"
          variant="solo"
        ></v-select>
      </v-col>
    </v-row>
    <v-data-table-server
      v-model:items-per-page="itemsPerPage"
      :headers="headers"
      :items="meteorites"
      :items-length="totalItems"
      :loading="loading"
      :items-per-page-options="[{ value: 10, title: '10' }]"
      @update:options="loadData"
    ></v-data-table-server>
  </v-container>
</template>

<script setup lang="ts">
import { onMounted, ref, watch, computed, reactive } from "vue";
import { MeteoriteApi } from "../services";
import { ResultMeteoriteGrouping, MeteoritesFiltersReq } from "../models";

const itemsPerPage = 10;
const headers = [
  { title: "Год", key: "year", align: "start" },
  { title: "Количество метеоритов", key: "count", align: "end" },
  { title: "Суммарная масса", key: "mass", align: "end" },
];

const totalItems = ref(0);
const years = computed(() => Array.from(Array(2101).keys()));
const loading = ref<boolean>(false);
const meteorites = ref<ResultMeteoriteGrouping>([]);
const allMeteoritesClasses = ref<string[]>([]);

const filters: MeteoritesFiltersReq = reactive({
  fromYear: 0,
  toYear: 2100,
  meteoriteName: "",
  meteoriteClass: "",
  sortableField: "year",
  isDesc: false,
  take: itemsPerPage,
  skip: 0,
});

async function updateData() {
  const api = new MeteoriteApi();
  const meteoritesForApi: ResultMeteoriteGrouping = await api.getMeteorites(
    filters
  );
  meteorites.value = meteoritesForApi;
  totalItems.value =
    meteoritesForApi.length != 0 ? meteoritesForApi[0].totalCount : 0;
}

function loadData({ page, sortBy }) {
  if (sortBy && sortBy.length) {
    const { key, order } = sortBy[0];
    filters.sortableField = key;
    setTypeSort(order);
  }
  filters.skip = itemsPerPage * page - itemsPerPage;
}

function setTypeSort(type: string) {
  if (type == "desc") {
    filters.isDesc = true;
    return;
  }
  filters.isDesc = false;
}

onMounted(async () => {
  const api = new MeteoriteApi();
  allMeteoritesClasses.value = await api.getMeteoritesClasses();
  filters.meteoriteClass = allMeteoritesClasses.value[0];
  await updateData();
});

watch([filters], async (newVal, oldVal) => {
  await updateData();
});
</script>

<style scoped>
.top-panel {
  margin: 24px;
}
</style>
