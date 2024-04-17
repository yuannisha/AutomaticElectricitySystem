<script setup lang="ts">
  import { ref, reactive, onMounted, onUnmounted, nextTick } from 'vue';
  // import { installationPlan } from "@/api";
  import { graphic } from 'echarts/core';
  import { ElMessage } from 'element-plus';
  import { BookingInformationsServiceProxy } from '/@/services/ServiceProxies';

  const option = ref({});
  // const getData = () => {
  //   installationPlan()
  //     .then((res) => {
  //       console.log('中下--教室使用情况概览', res);
  //       if (res.success) {
  //         setOption(res.data);
  //       } else {
  //         ElMessage({
  //           message: res.msg,
  //           type: 'warning',
  //         });
  //       }
  //     })
  //     .catch((err) => {
  //       ElMessage.error(err);
  //     });
  // };
  let IntervalTask: any;
  function scheduleHourlyTask() {
    const now = new Date();
    const nextHour = new Date(now);
    nextHour.setHours(now.getHours() + 1);
    nextHour.setMinutes(0, 0, 0); // 设置为下一个整点，分钟和秒都为0

    const delay = nextHour.getTime() - now.getTime(); // 下一个整点距离现在的毫秒数

    setTimeout(async () => {
      await getData(); // 执行任务

      // 设置每小时执行一次的定时器
      IntervalTask = setInterval(async () => {
        await getData();
      }, 1000 * 60 * 60); // 每小时
    }, delay);
  }
  scheduleHourlyTask();
  const getData = async () => {
    var service = new BookingInformationsServiceProxy();
    var datas = await service.getRoomsUsedCondition();
    setOption(datas);
  };
  const setOption = async (newData: any) => {
    option.value = {
      tooltip: {
        trigger: 'axis',
        backgroundColor: 'rgba(0,0,0,.6)',
        borderColor: 'rgba(147, 235, 248, .8)',
        textStyle: {
          color: '#FFF',
        },
        formatter: function (params: any) {
          // 添加单位
          var result = params[1].name + '<br>';
          params.forEach(function (item: any) {
            if (item.value) {
              if (item.seriesName == '使用率') {
                result += item.marker + ' ' + item.seriesName + ' : ' + item.value + '%</br>';
              } else {
                result += item.marker + ' ' + item.seriesName + ' : ' + item.value + '个</br>';
              }
            } else {
              result += item.marker + ' ' + item.seriesName + ' :  - </br>';
            }
          });
          return result;

        },
      },
      legend: {
        data: ['使用数', '使用率'],
        textStyle: {
          color: '#B4B4B4',
        },
        top: '0',
      },
      grid: {
        left: '50px',
        right: '40px',
        bottom: '30px',
        top: '20px',
      },
      xAxis: {
        data: newData.lastTwelveHours,
        axisLine: {
          lineStyle: {
            color: '#B4B4B4',
          },
        },
        axisTick: {
          show: false,
        },
      },
      yAxis: [
        {
          splitLine: { show: false },
          axisLine: {
            lineStyle: {
              color: '#B4B4B4',
            },
          },

          axisLabel: {
            formatter: '{value}',
          },
        },
        {
          splitLine: { show: false },
          axisLine: {
            lineStyle: {
              color: '#B4B4B4',
            },
          },
          axisLabel: {
            formatter: '{value} %',
          },
        },
      ],
      series: [
        {
          name: '使用数',
          type: 'bar',
          barWidth: 10,
          itemStyle: {
            borderRadius: 5,
            color: new graphic.LinearGradient(0, 0, 0, 1, [
              { offset: 0, color: '#956FD4' },
              { offset: 1, color: '#3EACE5' },
            ]),
          },
          data: newData.usedCountAmount,
        },
        {
          name: '使用率',
          type: 'line',
          smooth: true,
          showAllSymbol: true,
          symbol: 'emptyCircle',
          symbolSize: 8,
          yAxisIndex: 1,
          itemStyle: {
            color: '#F02FC2',
          },
          data: newData.useRatesOfRooms,
        },
      ],
    };
  };
  onMounted(async () => {
    await getData();
    onUnmounted(() => {
      clearInterval(IntervalTask);
    });
  });
</script>

<template>
  <v-chart class="chart" :option="option" v-if="JSON.stringify(option) != '{}'" />
</template>

<style scoped lang="scss"></style>
