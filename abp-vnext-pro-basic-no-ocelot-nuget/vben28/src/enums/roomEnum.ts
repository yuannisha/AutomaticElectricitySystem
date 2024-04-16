enum RoomTypeEnum {
  DefaultValue = 0,
  NormalRoom = 3,
  MultiMediaRoom = 1,
  ComputerRoom = 2,
}

enum ControlTypeEnum {
  DefaultValue = 0,
  Auto = 2,
  Manual = 1,
}

enum UsingOrNot {
  DefaultValue = 0,
  IsNotUsing = 2,
  IsUsing = 1,
}

// 映射对象
const RoomTypeEnumLabels: { [key in RoomTypeEnum]: string } = {
  [RoomTypeEnum.DefaultValue]: '默认',
  [RoomTypeEnum.NormalRoom]: '普通教室',
  [RoomTypeEnum.MultiMediaRoom]: '多媒体教室',
  [RoomTypeEnum.ComputerRoom]: '机房',
};

// 映射对象
const ControlTypeEnumLabels: { [key in ControlTypeEnum]: string } = {
  [ControlTypeEnum.DefaultValue]: '默认',
  [ControlTypeEnum.Auto]: '自动',
  [ControlTypeEnum.Manual]: '手动',
};

// 映射对象
const UsingOrNotEnumLabels: { [key in UsingOrNot]: string } = {
  [UsingOrNot.DefaultValue]: '默认',
  [UsingOrNot.IsNotUsing]: '未使用',
  [UsingOrNot.IsUsing]: '正在使用',
};

// 创建从字符串到 RoomTypeEnum 的反向映射
const reversedRoomTypeEnumLabels = Object.entries(RoomTypeEnumLabels).reduce(
  (acc, [key, value]) => {
    acc[value] = Number(key);
    return acc;
  },
  {} as { [key: string]: RoomTypeEnum },
);

// 创建一个从字符串到ControlType的反向映射
const reversedControlTypeEnumLabels = Object.entries(ControlTypeEnumLabels).reduce(
  (acc, [key, value]) => {
    acc[value] = Number(key);
    return acc;
  },
  {} as { [key: string]: ControlTypeEnum },
);

// 创建从字符串到 UsingOrNotEnum 的反向映射
const reversedUsingOrNotEnumLabels = Object.entries(UsingOrNotEnumLabels).reduce(
  (acc, [key, value]) => {
    acc[value] = Number(key);
    return acc;
  },
  {} as { [key: string]: UsingOrNot },
);

export function getRoomTypeLabel(RoomType: number): string {
  // 将字符串参数转换为数字
  // const typeNumber = parseInt(RoomType, 10);
  // 获取映射对象的值
  const label = RoomTypeEnumLabels[RoomType];
  // 如果label存在，则返回它；否则返回一个默认值或错误消息
  return label || '未知类型';
}

export function getControlTypeLabel(ControlType: number): string {
  // 将字符串参数转换为数字
  // const typeNumber = parseInt(ControlType, 10);
  // 获取映射对象的值
  const label = ControlTypeEnumLabels[ControlType];
  // 如果label存在，则返回它；否则返回一个默认值或错误消息
  return label || '未知类型';
}

export function getUsingOrNotEnumLabel(UsingOrNot: number): string {
  // 将字符串参数转换为数字
  // const typeNumber = parseInt(UsingOrNot, 10);
  // 获取映射对象的值
  const label = UsingOrNotEnumLabels[UsingOrNot];
  // 如果label存在，则返回它；否则返回一个默认值或错误消息
  return label || '未知类型';
}

// 函数，用于根据字符串获取枚举值
export function getControlTypeEnumValue(label: string): ControlTypeEnum | undefined {
  return reversedControlTypeEnumLabels[label];
}

export function getRoomTypeEnumValue(label: string): RoomTypeEnum | undefined {
  return reversedRoomTypeEnumLabels[label];
}

export function getUsingOrNotEnumValue(label: string): UsingOrNot | undefined {
  return reversedUsingOrNotEnumLabels[label];
}

// 调用示例
// const normalRoomValue = getRoomTypeEnumValue('普通教室'); // 应返回 0
// const isUsingValue = getUsingOrNotEnumValue('正在使用'); // 应返回 1

// console.log(normalRoomValue, isUsingValue); // 输出: 0 1
