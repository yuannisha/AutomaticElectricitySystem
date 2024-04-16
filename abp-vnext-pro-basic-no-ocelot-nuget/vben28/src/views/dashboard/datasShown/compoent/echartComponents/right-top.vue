<script setup lang="ts">
  import { ref, onMounted, onUnmounted } from 'vue';
  // import { alarmNum } from "@/api";
  import { graphic } from 'echarts/core';
  import { ElMessage } from 'element-plus';
  import { DailyTotalConsumptionServiceProxy } from '/@/services/ServiceProxies';

  const option = ref({});
  //从这往下都是临时数据
  const data = {
    dateList: null,
    numList: null,
  };

  //从这往上都是临时数据

  //从这往下都是写好的函数暂时注释的
  // const data = {
  //   // dateList: ['2024-4-', '2021-12', '2022-01', '2022-02', '2022-03', '2022-04'],
  //   // numList: [456, 584, 662, 34, 299, 334],
  //   dateList: [],
  //   numList: [],
  // };
  // const option = ref(data);
  // onMounted(async () => {
  //   await getDatasForData();
  //   const getDatas = setInterval(async () => {
  //     await getDatasForData();
  //   }, 1000 * 60 * 15);
  //   onUnmounted(() => {
  //     clearInterval(getDatas);
  //   });
  // });

  // async function getDatasForData() {
  //   const service = new ConsumptionAmountServiceProxy();
  //   var datas = await service.getConsumptionAmountWithLastSevenDays();
  //   datas.consumptionAmountOutput.forEach((x) => {
  //     data.dateList.push(x.date);
  //     data.numList.push(x.powerConsumption);
  //   });
  //   setOption(data.dateList, data.numList);
  // }
  //从这往下都是写好的函数真正要用的暂时注释的
  // onMounted(() => {
  //   await getDatasForData();
  // });
  //   async function scheduleTask() {
  //   // console.log('任务执行了', new Date().toLocaleTimeString());
  //   // 在这里执行您的任务
  //   await getDatasForData();
  // }

  async function getDatasForData() {
    const service = new DailyTotalConsumptionServiceProxy();
    var datas = await service.getDailyTotalConsumptionWithLastSevenDays();
    data.dateList = datas.dates;
    data.numList = datas.powerConsumption;
    setOption(data.dateList, data.numList);
  }

  async function scheduleTask() {
    await getDatasForData();
  }

  let setIntervalTask: any;
  function startScheduledTask() {
    // 获取当前时间
    const now = new Date();
    // 创建一个新的时间对象，表示今天的凌晨 00:00
    const midnight = new Date(now.getFullYear(), now.getMonth(), now.getDate() + 1, 0, 0, 0);
    // 计算当前时间到凌晨的毫秒数
    const firstDelay = midnight.getTime() - now.getTime();

    // 设置一个 timeout 来在今天的凌晨 00:00 触发
    setTimeout(async () => {
      await scheduleTask(); // 执行任务

      // 之后，使用 setInterval 每 24 小时执行一次任务
      setIntervalTask = setInterval(scheduleTask, 24 * 60 * 60 * 1000);
    }, firstDelay);
  }
  onMounted(async () => {
    await getDatasForData();
    onUnmounted(() => {
      clearInterval(setIntervalTask);
    });
  });

  // // function startScheduledTask() {
  // //   const now = new Date();
  // //   // 计算下一个20分钟整点的时间
  // //   const delay = (20 - (now.getMinutes() % 20)) * 60 * 1000 - now.getSeconds() * 1000 - now.getMilliseconds();

  // //   // 设置第一次执行的延迟
  // //   setTimeout(() => {
  // //     scheduleTask(); // 执行任务
  // //     // 之后每20分钟执行一次
  // //     setInterval(scheduleTask, 20 * 60 * 1000);
  // //   }, delay);
  // // }

  // // 启动定时任务
  startScheduledTask();
  //从这往上都是写好的函数真正要用的暂时注释的
  //从这往上都是写好的函数暂时注释的

  // function getLastSevenDays() {
  //   const dates: string[] = [];
  //   const today = new Date();
  //   for (let i = 0; i < 7; i++) {
  //     const pastDate = new Date(today);
  //     pastDate.setDate(pastDate.getDate() - i);
  //     dates.push(`${pastDate.getFullYear()}-${pastDate.getMonth() + 1}-${pastDate.getDate()}`);
  //   }
  //   data.dateList = dates;
  // }
  // function getLastSevenDays(): string[] {
  //   const dates: string[] = [];
  //   const today = new Date();
  //   for (let i = 0; i < 7; i++) {
  //     const pastDate = new Date(today);
  //     pastDate.setDate(pastDate.getDate() - i);
  //     dates.push(`${pastDate.getFullYear()}-${pastDate.getMonth() + 1}-${pastDate.getDate()}`);
  //   }
  //   return dates;
  // }

  // const scheduleDailyTask = (task) => {
  //   const now = new Date();
  //   const nextMidnight = new Date(now.getFullYear(), now.getMonth(), now.getDate() + 1, 0, 0, 0);
  //   const delay = nextMidnight - now;

  //   setTimeout(() => {
  //     task();
  //     // 完成后，重新安排
  //     scheduleDailyTask(task);
  //   }, delay);
  // };

  // const myTask = () => {
  //   console.log('每天00:00执行的任务');
  // };

  // scheduleDailyTask(myTask);

  // // Example usage
  // console.log(getLastSevenDays());

  // const getData = () => {
  //   alarmNum()
  //     .then((res) => {
  //       console.log("右上--报警次数 ", res);
  //       if (res.success) {
  //         setOption(res.data.dateList, res.data.numList, res.data.numList2);
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
  const setOption = async (xData: any[], yData: any[]) => {
    option.value = {
      xAxis: {
        type: 'category',
        data: xData,
        boundaryGap: false, // 不留白，从原点开始
        splitLine: {
          show: true,
          lineStyle: {
            color: 'rgba(31,99,163,.2)',
          },
        },
        axisLine: {
          // show:false,
          lineStyle: {
            color: 'rgba(31,99,163,.1)',
          },
        },
        axisLabel: {
          color: '#7EB7FD',
          fontWeight: '500',
        },
      },
      yAxis: {
        type: 'value',
        splitLine: {
          show: true,
          lineStyle: {
            color: 'rgba(31,99,163,.2)',
          },
        },
        axisLine: {
          lineStyle: {
            color: 'rgba(31,99,163,.1)',
          },
        },
        axisLabel: {
          color: '#7EB7FD',
          fontWeight: '500',
        },
      },
      tooltip: {
        trigger: 'axis',
        backgroundColor: 'rgba(0,0,0,.6)',
        borderColor: 'rgba(147, 235, 248, .8)',
        textStyle: {
          color: '#FFF',
        },
      },
      grid: {
        //布局
        show: true,
        left: '10px',
        right: '30px',
        bottom: '10px',
        top: '32px',
        containLabel: true,
        borderColor: '#1F63A3',
      },
      series: [
        {
          data: yData,
          type: 'line',
          smooth: true,
          symbol: 'none', //去除点
          name: '消耗电量(°)',
          color: 'rgba(252,144,16,.7)',
          areaStyle: {
            //右，下，左，上
            color: new graphic.LinearGradient(
              0,
              0,
              0,
              1,
              [
                {
                  offset: 0,
                  color: 'rgba(252,144,16,.7)',
                },
                {
                  offset: 1,
                  color: 'rgba(252,144,16,.0)',
                },
              ],
              false,
            ),
          },
          markPoint: {
            data: [
              {
                name: '最大值',
                type: 'max',
                valueDim: 'y',
                symbol: 'rect',
                symbolSize: [60, 26],
                symbolOffset: [0, -20],
                itemStyle: {
                  color: 'rgba(0,0,0,0)',
                },
                label: {
                  color: '#FC9010',
                  backgroundColor: 'rgba(252,144,16,0.1)',
                  borderRadius: 6,
                  padding: [7, 14],
                  borderWidth: 0.5,
                  borderColor: 'rgba(252,144,16,.5)',
                  formatter: '消耗电量：{c}°',
                },
              },
              {
                name: '最大值',
                type: 'max',
                valueDim: 'y',
                symbol: 'circle',
                symbolSize: 6,
                itemStyle: {
                  color: '#FC9010',
                  shadowColor: '#FC9010',
                  shadowBlur: 8,
                },
                label: {
                  formatter: '',
                },
              },
            ],
          },
        },
      ],
    };
  };
  // onMounted(() => {
  //   // scheduleDailyTask(getLastSevenDays); //每天00:00的时候更新日期数组
  //   // getData();
  //   const getPowerSwitchsNum = setInterval(async () =>{

  //   });

  //   setOption(data.dateList, data.numList);
  // });
</script>

<template>
  <v-chart class="chart" :option="option" v-if="JSON.stringify(option) != '{}'" />
</template>

<style scoped lang="scss"></style>
