﻿namespace YAMP.Sensors.Devices
{
    using System;
    using Windows.Devices.Sensors;
    using Sensor = Windows.Devices.Sensors.Gyrometer;

    /// <summary>
    /// The gyrometer device.
    /// </summary>
    public class Gyrometer : BaseDevice
    {
        static readonly Sensor sensor = GetSensor();

        private static Sensor GetSensor()
        {
            try
            {
                return Sensor.GetDefault();
            }
            catch
            {
                return null;
            }
        }

        private event EventHandler<GyrometerEventArgs> changed;

        /// <summary>
        /// Listens to the changed event.
        /// </summary>
        public event EventHandler<GyrometerEventArgs> Changed
        {
            add
            {
                InstallHandler(sensor);
                changed += value;
            }
            remove
            {
                changed -= value;
                UninstallHandler(sensor);
            }
        }

        /// <summary>
        /// Installs the reading handler.
        /// </summary>
        protected override void InstallReadingChangedHandler()
        {
            sensor.ReadingChanged += OnReadingChanged;
        }

        /// <summary>
        /// Uninstalls the reading handler.
        /// </summary>
        protected override void UninstallReadingChangedHandler()
        {
            sensor.ReadingChanged -= OnReadingChanged;
        }

        void OnReadingChanged(Sensor sender, GyrometerReadingChangedEventArgs args)
        {
            var handler = changed;

            if (handler != null)
            {
                var value = ConvertToVector(args.Reading);
                var e = new GyrometerEventArgs(value);
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// Gets the current angular velocity.
        /// </summary>
        public Vector CurrentAngularVelocity
        {
            get
            {
                if (sensor != null)
                {
                    var eading = sensor.GetCurrentReading();
                    return ConvertToVector(eading);
                }

                return new Vector();
            }
        }

        static Vector ConvertToVector(GyrometerReading reading)
        {
            return new Vector
            {
                X = reading.AngularVelocityX,
                Y = reading.AngularVelocityY,
                Z = reading.AngularVelocityZ
            };
        }
    }
}
