<template>
  <v-container>
    <v-row class="top-panel">
      <v-col>
        <v-select
          v-model="fromYear"
          label="С"
          :items="years"
          density="compact"
          variant="solo"
        ></v-select>
      </v-col>
      <v-col>
        <v-select
          v-model="toYear"
          label="По"
          :items="years"
          density="compact"
          variant="solo"
        ></v-select>
      </v-col>
      <v-col>
        <v-text-field
          v-model="nameMeteorite"
          label="Название метеорита"
          density="compact"
          variant="solo"
        ></v-text-field>
      </v-col>
      <v-col>
        <v-select
          v-model="selectedСlass"
          label="Класс метеорита"
          :items="classes"
          density="compact"
          variant="solo"
        ></v-select>
      </v-col>
    </v-row>
    <v-data-table-server
      v-model:items-per-page="itemsPerPage"
      :headers="headers"
      :items="items"
      :items-length="totalItems"
      :loading="loading"
      :items-per-page-options="[{ value: 10, title: '10' }]"
      @update:options="loadData"
    ></v-data-table-server>
  </v-container>
</template>

<script setup lang="ts">
import { onMounted, ref, watch, computed } from "vue";
import { MeteoriteApi } from "../api";
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
const fromYear = ref<number>(0);
const toYear = ref<number>(2100);
const items = ref<ResultMeteoriteGrouping>([]);
const classes = ref<string[]>([]);
const selectedСlass = ref<string>("");
const sortableField = ref<string>("year");
const nameMeteorite = ref<string>("");
const skip = ref<number>(0);
const isDesc = ref<boolean>(false);

async function updateData() {
  const api = new MeteoriteApi();
  const meteorites: ResultMeteoriteGrouping = await api.getMeteorites({
    fromYear: fromYear.value,
    toYear: toYear.value,
    meteoriteName: nameMeteorite.value,
    meteoriteClass: selectedСlass.value,
    sortableField: sortableField.value,
    isDesc: isDesc.value,
    take: itemsPerPage,
    skip: skip.value,
  } as MeteoritesFiltersReq);
  items.value = meteorites;
  totalItems.value = meteorites.length != 0 ? meteorites[0].totalCount : 0;
}

function loadData({ page, sortBy }) {
  if (sortBy && sortBy.length) {
    const { key, order } = sortBy[0];
    sortableField.value = key;
    setTypeSort(order);
  }
  skip.value = itemsPerPage * page - itemsPerPage;
}

function setTypeSort(type: string) {
  if (type == "desc") {
    isDesc.value = true;
  } else {
    isDesc.value = false;
  }
}

onMounted(async () => {
  const api = new MeteoriteApi();
  classes.value = await api.getMeteoritesClasses();
  selectedСlass.value = classes.value[0];
  await updateData();
});

watch(
  [sortableField, selectedСlass, nameMeteorite, fromYear, toYear, isDesc, skip],
  async (newVal, oldVal) => {
    await updateData();
  }
);
</script>

<style scoped>
.top-panel {
  margin: 24px;
}
</style>
