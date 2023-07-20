using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicZaanVendor.Common
{
    
    public enum Month
    {
        January = 1,

        February = 2,

        March = 3,

        April = 4,

        May = 5,

        June = 6,

        July = 7,

        August = 8,

        September = 9,

        October = 10,

        November = 11,

        December = 12,
    }

    public enum LeaveType
    {
        [Description("Full Day")]
        FullDay = 1,

        [Description("First Half")]
        FirstHalf = 2,

        [Description("Second Half")]
        SecondHalf = 3
    }

    /// <summary>
    /// Device/Channel alarm states
    /// </summary>
    public enum AlarmState
    {
        /// <summary>
        /// Indicates device alarm when channel reading has gone out of the Min/Max range
        /// </summary>
        AlarmOut = 0,

        /// <summary>
        /// Indicates device alarm when channel reading has returned within the Min/Max range
        /// </summary>
        AlarmIn,

        /// <summary>
        /// Indicates that an alarm has been acknowledged on the device
        /// </summary>
        AlarmAcknowledge,

        /// <summary>
        /// Indicates a low battery alarm
        /// </summary>
        LowBattery,

        /// <summary>
        /// Indicates a potential lost connectivity condition. This alarm is thrown
        /// by the cloud service after 2 missed update intervals.
        /// </summary>
        LostConnectivity,

        /// <summary>
        /// Indicates no alarm on the channel
        /// </summary>
        NoAlarm,

        /// <summary>
        /// Indicates when channel reading has exceeded the device Max alarm setting
        /// </summary>
        MaxAlarmOut,

        /// <summary>
        /// Indicates when channel reading has exceeded the device Min alaram setting
        /// </summary>
        MinAlarmOut,

        /// <summary>
        /// Indicates when the channel reading has reentered the valid range from outside the upper bound
        /// </summary>
        MaxAlarmIn,

        /// <summary>
        /// Indicates when the channel reading has reentered the valid range from outside the lower bound
        /// </summary>
        MinAlarmIn,

        /// <summary>
        /// Indicates when device connectivity is restored
        /// </summary>
        ConnectvityRestored,

        /// <summary>
        /// Indicates when a channel's alarm setting has been changed
        /// </summary>
        SettingChanged,

        /// <summary>
        /// Indicates that a user has physically checked the device by pushing the alarm acknowledge button
        /// while the device is not in alarm.
        /// </summary>
        DeviceChecked,

        /// <summary>
        /// Indicates that a user has pushed the start button on a non-connected logging device to commence data logging
        /// </summary>
        LoggingStarted,

        /// <summary>
        /// Indicates a pump has gone offline
        /// </summary>
        PumpOffline,

        /// <summary>
        /// Indicates dispense mode has been turned on
        /// </summary>
        DispenseOn,

        /// <summary>
        /// Indicates dispense mode has been turned off
        /// </summary>
        DispenseOff,

        /// <summary>
        /// Indicates the pump head has been opened
        /// </summary>
        PumpHeadOpen,

        /// <summary>
        /// Indicates the auxillary input has been opened
        /// </summary>
        AuxInputOpen,

        /// <summary>
        /// Indicates the auxillary input has been closed
        /// </summary>
        AuxInputClosed,

        /// <summary>
        /// Pump has sent an error status message
        /// </summary>
        PumpError,

        /// <summary>
        /// Pump has had its power interrupted
        /// </summary>
        PowerInterupted
    }

    public enum UiType
    {
        /// <summary>
        /// BeautyBook
        /// </summary>
        BeautyBook = 0,

        /// <summary>
        /// MasterflexLive
        /// </summary>
        MasterflexLive = 1,

        /// <summary>
        /// CpLive
        /// </summary>
        CpLive = 2
    }

    public enum ServiceLevel
    {
        /// <summary>
        /// Subscription level that is free as in "free beer."
        /// </summary>
        Free,

        /// <summary>
        /// Standard subscription
        /// </summary>
        Standard,


        /// <summary>
        /// Subscription with extra bells and whistles
        /// </summary>
        Premium
    }

}
