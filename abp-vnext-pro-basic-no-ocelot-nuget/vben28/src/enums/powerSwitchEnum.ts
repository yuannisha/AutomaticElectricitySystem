enum IsOnlineEnum {
  DefaultValue = 0,
  On = 1,
  Off = 2,
}

enum StatusEnum {
  DefaultValue = 0,
  Open = 1,
  Close = 2,
}

enum IsAbnormalEnum {
  DefaultValue = 0,
  IsAbnormal = 1,
  IsNotAbnormal = 2,
}

// 映射对象
const StatusEnumLabels: { [key in StatusEnum]: string } = {
  [StatusEnum.DefaultValue]: '默认',
  [StatusEnum.Close]: '关闭',
  [StatusEnum.Open]: '打开',
};

// 映射对象
const IsOnlineEnumLabels: { [key in IsOnlineEnum]: string } = {
  [IsOnlineEnum.DefaultValue]: '默认',
  [IsOnlineEnum.On]: '在线',
  [IsOnlineEnum.Off]: '离线',
};

// 映射对象
const IsAbnormalEnumLabels: { [key in IsAbnormalEnum]: string } = {
  [IsAbnormalEnum.DefaultValue]: '默认',
  [IsAbnormalEnum.IsAbnormal]: '异常',
  [IsAbnormalEnum.IsNotAbnormal]: '正常',
};

// 创建从字符串到 StatusEnum 的反向映射
const reversedStatusEnumLabels = Object.entries(StatusEnumLabels).reduce((acc, [key, value]) => {
  acc[value] = Number(key);
  return acc;
}, {} as { [key: string]: StatusEnum });

// 创建从字符串到 IsOnlineEnum 的反向映射
const reversedIsOnlineEnumLabels = Object.entries(IsOnlineEnumLabels).reduce(
  (acc, [key, value]) => {
    acc[value] = Number(key);
    return acc;
  },
  {} as { [key: string]: IsOnlineEnum },
);
// 创建从字符串到 IsOnlineEnum 的反向映射
const reversedIsAbnormalEnumLabels = Object.entries(IsAbnormalEnumLabels).reduce(
  (acc, [key, value]) => {
    acc[value] = Number(key);
    return acc;
  },
  {} as { [key: string]: IsAbnormalEnum },
);

export function getIsOnlinepeLabel(IsOnline: number): string {
  // 将字符串参数转换为数字
  // const typeNumber = parseInt(IsOnline, 10);
  // 获取映射对象的值
  const label = IsOnlineEnumLabels[IsOnline];
  // 如果label存在，则返回它；否则返回一个默认值或错误消息
  return label || '未知类型';
}

export function getStatusLabel(Status: number): string {
  // 将字符串参数转换为数字
  // const typeNumber = parseInt(Status, 10);
  // 获取映射对象的值
  const label = StatusEnumLabels[Status];
  // 如果label存在，则返回它；否则返回一个默认值或错误消息
  return label || '未知类型';
}

export function getIsAbnormalLabel(Status: number): string {
  // 将字符串参数转换为数字
  // const typeNumber = parseInt(Status, 10);
  // 获取映射对象的值
  const label = IsAbnormalEnumLabels[Status];
  // 如果label存在，则返回它；否则返回一个默认值或错误消息
  return label || '未知类型';
}

// 函数，用于根据字符串获取枚举值
export function getStatusEnumValue(label: string): StatusEnum | undefined {
  return reversedStatusEnumLabels[label];
}

export function getIsOnlineEnumValue(label: string): IsOnlineEnum | undefined {
  return reversedIsOnlineEnumLabels[label];
}

export function getIsAbnormalEnumValue(label: string): IsAbnormalEnum | undefined {
  return reversedIsAbnormalEnumLabels[label];
}
