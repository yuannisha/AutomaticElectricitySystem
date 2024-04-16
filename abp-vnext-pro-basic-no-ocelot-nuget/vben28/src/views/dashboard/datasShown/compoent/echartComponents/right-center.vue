<script setup lang="ts">
  import { ref, reactive, onMounted, onUnmounted } from 'vue';
  import CapsuleChart from '../datav/capsule-chart';
  // import { ranking } from "@/api";
  import { ElMessage } from 'element-plus';
  import { BuildingConsumptionServiceProxy } from '/@/services/ServiceProxies';

  const config = ref({
    showValue: true,
    unit: '次',
  });
  const data = ref([]);
  // const data = ref([
  //   { value: 986, name: '离岛' },
  //   { value: 940, name: '安顺市' },
  //   { value: 799, name: '开封市' },
  //   { value: 761, name: '杭州市' },
  //   { value: 439, name: '深圳市' },
  //   { value: 402, name: '资阳市' },
  //   { value: 255, name: '江门市' },
  //   { value: 189, name: '宝鸡市' },
  // ]);

  async function getDatasForData() {
    const service = new BuildingConsumptionServiceProxy();
    var datas = await service.getBuildingConsumptionRankOutput();
    // 将新数据转换为期望的格式
    const transformedData = datas.buildingConsumptionRankOutput.map((item) => ({
      value: item.powerConsumption,
      name: item.buildingName,
    }));
    data.value = transformedData;
  }
  onMounted(async () => {
    await getDatasForData(); //启动时获取数据
  });

  async function scheduleTask() {
    // console.log('任务执行了', new Date().toLocaleTimeString());
    // 在这里执行您的任务
    await getDatasForData();
  }

  function startScheduledTask() {
    const now = new Date();
    // 计算下一个20分钟整点的时间
    const delay =
      (20 - (now.getMinutes() % 20)) * 60 * 1000 - now.getSeconds() * 1000 - now.getMilliseconds();

    // 设置第一次执行的延迟
    setTimeout(() => {
      scheduleTask(); // 执行任务
      // 之后每20分钟执行一次
      setInterval(scheduleTask, 20 * 60 * 1000);
    }, delay);
  }

  // 启动定时任务
  startScheduledTask();
  // const getData = () => {
  //   ranking()
  //     .then((res) => {
  //       console.log("右中--报警排名", res);
  //       if (res.success) {
  //         data.value = res.data;
  //       } else {
  //         ElMessage({
  //           message: res.msg,
  //           type: "warning",
  //         });
  //       }
  //     })
  //     .catch((err) => {
  //       ElMessage.error(err);
  //     });
  // };
  // getData();
</script>

<template>
  <div class="right_bottom">
    <CapsuleChart :config="config" style="width: 100%; height: 260px" :data="data" />
  </div>
</template>

<style scoped lang="scss">
  .right_bottom {
    box-sizing: border-box;
    padding: 0 16px;
  }
</style>
