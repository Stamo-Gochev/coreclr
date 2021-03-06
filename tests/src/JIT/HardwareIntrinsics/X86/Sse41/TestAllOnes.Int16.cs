// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\X86\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace JIT.HardwareIntrinsics.X86
{
    public static partial class Program
    {
        private static void TestAllOnesInt16()
        {
            var test = new BooleanUnaryOpTest__TestAllOnesInt16();

            if (test.IsSupported)
            {
                // Validates basic functionality works, using Unsafe.Read
                test.RunBasicScenario_UnsafeRead();

                if (Sse2.IsSupported)
                {
                    // Validates basic functionality works, using Load
                    test.RunBasicScenario_Load();

                    // Validates basic functionality works, using LoadAligned
                    test.RunBasicScenario_LoadAligned();
                }

                // Validates calling via reflection works, using Unsafe.Read
                test.RunReflectionScenario_UnsafeRead();

                if (Sse2.IsSupported)
                {
                    // Validates calling via reflection works, using Load
                    test.RunReflectionScenario_Load();

                    // Validates calling via reflection works, using LoadAligned
                    test.RunReflectionScenario_LoadAligned();
                }

                // Validates passing a static member works
                test.RunClsVarScenario();

                if (Sse2.IsSupported)
                {
                    // Validates passing a static member works, using pinning and Load
                    test.RunClsVarScenario_Load();
                }

                // Validates passing a local works, using Unsafe.Read
                test.RunLclVarScenario_UnsafeRead();

                if (Sse2.IsSupported)
                {
                    // Validates passing a local works, using Load
                    test.RunLclVarScenario_Load();

                    // Validates passing a local works, using LoadAligned
                    test.RunLclVarScenario_LoadAligned();
                }

                // Validates passing the field of a local class works
                test.RunClassLclFldScenario();

                if (Sse2.IsSupported)
                {
                    // Validates passing the field of a local class works, using pinning and Load
                    test.RunClassLclFldScenario_Load();
                }

                // Validates passing an instance member of a class works
                test.RunClassFldScenario();

                if (Sse2.IsSupported)
                {
                    // Validates passing an instance member of a class works, using pinning and Load
                    test.RunClassFldScenario_Load();
                }

                // Validates passing the field of a local struct works
                test.RunStructLclFldScenario();

                if (Sse2.IsSupported)
                {
                    // Validates passing the field of a local struct works, using pinning and Load
                    test.RunStructLclFldScenario_Load();
                }

                // Validates passing an instance member of a struct works
                test.RunStructFldScenario();

                if (Sse2.IsSupported)
                {
                    // Validates passing an instance member of a struct works, using pinning and Load
                    test.RunStructFldScenario_Load();
                }
            }
            else
            {
                // Validates we throw on unsupported hardware
                test.RunUnsupportedScenario();
            }

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class BooleanUnaryOpTest__TestAllOnesInt16
    {
        private struct DataTable
        {
            private byte[] inArray1;

            private GCHandle inHandle1;

            private ulong alignment;

            public DataTable(Int16[] inArray1, int alignment)
            {
                int sizeOfinArray1 = inArray1.Length * Unsafe.SizeOf<Int16>();
                if ((alignment != 32 && alignment != 16) || (alignment * 2) < sizeOfinArray1)
                {
                    throw new ArgumentException("Invalid value of alignment");
                }

                this.inArray1 = new byte[alignment * 2];

                this.inHandle1 = GCHandle.Alloc(this.inArray1, GCHandleType.Pinned);

                this.alignment = (ulong)alignment;

                Unsafe.CopyBlockUnaligned(ref Unsafe.AsRef<byte>(inArray1Ptr), ref Unsafe.As<Int16, byte>(ref inArray1[0]), (uint)sizeOfinArray1);
            }

            public void* inArray1Ptr => Align((byte*)(inHandle1.AddrOfPinnedObject().ToPointer()), alignment);

            public void Dispose()
            {
                inHandle1.Free();
            }

            private static unsafe void* Align(byte* buffer, ulong expectedAlignment)
            {
                return (void*)(((ulong)buffer + expectedAlignment - 1) & ~(expectedAlignment - 1));
            }
        }

        private struct TestStruct
        {
            public Vector128<Int16> _fld1;

            public static TestStruct Create()
            {
                var testStruct = new TestStruct();

                for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetInt16(); }
                Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int16>, byte>(ref testStruct._fld1), ref Unsafe.As<Int16, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Int16>>());

                return testStruct;
            }

            public void RunStructFldScenario(BooleanUnaryOpTest__TestAllOnesInt16 testClass)
            {
                var result = Sse41.TestAllOnes(_fld1);
                testClass.ValidateResult(_fld1, result);
            }

            public void RunStructFldScenario_Load(BooleanUnaryOpTest__TestAllOnesInt16 testClass)
            {
                fixed (Vector128<Int16>* pFld1 = &_fld1)
                {
                    var result = Sse41.TestAllOnes(
                        Sse2.LoadVector128((Int16*)(pFld1))
                    );

                    testClass.ValidateResult(_fld1, result);
                }
            }
        }

        private static readonly int LargestVectorSize = 16;

        private static readonly int Op1ElementCount = Unsafe.SizeOf<Vector128<Int16>>() / sizeof(Int16);

        private static Int16[] _data1 = new Int16[Op1ElementCount];

        private static Vector128<Int16> _clsVar1;

        private Vector128<Int16> _fld1;

        private DataTable _dataTable;

        static BooleanUnaryOpTest__TestAllOnesInt16()
        {
            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetInt16(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int16>, byte>(ref _clsVar1), ref Unsafe.As<Int16, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Int16>>());
        }

        public BooleanUnaryOpTest__TestAllOnesInt16()
        {
            Succeeded = true;

            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetInt16(); }
            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Vector128<Int16>, byte>(ref _fld1), ref Unsafe.As<Int16, byte>(ref _data1[0]), (uint)Unsafe.SizeOf<Vector128<Int16>>());

            for (var i = 0; i < Op1ElementCount; i++) { _data1[i] = TestLibrary.Generator.GetInt16(); }
            _dataTable = new DataTable(_data1, LargestVectorSize);
        }

        public bool IsSupported => Sse41.IsSupported;

        public bool Succeeded { get; set; }

        public void RunBasicScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_UnsafeRead));

            var result = Sse41.TestAllOnes(
                Unsafe.Read<Vector128<Int16>>(_dataTable.inArray1Ptr)
            );

            ValidateResult(_dataTable.inArray1Ptr, result);
        }

        public void RunBasicScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_Load));

            var result = Sse41.TestAllOnes(
                Sse2.LoadVector128((Int16*)(_dataTable.inArray1Ptr))
            );

            ValidateResult(_dataTable.inArray1Ptr, result);
        }

        public void RunBasicScenario_LoadAligned()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario_LoadAligned));

            var result = Sse41.TestAllOnes(
                Sse2.LoadAlignedVector128((Int16*)(_dataTable.inArray1Ptr))
            );

            ValidateResult(_dataTable.inArray1Ptr, result);
        }

        public void RunReflectionScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_UnsafeRead));

            var result = typeof(Sse41).GetMethod(nameof(Sse41.TestAllOnes), new Type[] { typeof(Vector128<Int16>) })
                                     .Invoke(null, new object[] {
                                        Unsafe.Read<Vector128<Int16>>(_dataTable.inArray1Ptr)
                                     });

            ValidateResult(_dataTable.inArray1Ptr, (bool)(result));
        }

        public void RunReflectionScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_Load));

            var result = typeof(Sse41).GetMethod(nameof(Sse41.TestAllOnes), new Type[] { typeof(Vector128<Int16>) })
                                     .Invoke(null, new object[] {
                                        Sse2.LoadVector128((Int16*)(_dataTable.inArray1Ptr))
                                     });

            ValidateResult(_dataTable.inArray1Ptr, (bool)(result));
        }

        public void RunReflectionScenario_LoadAligned()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario_LoadAligned));

            var result = typeof(Sse41).GetMethod(nameof(Sse41.TestAllOnes), new Type[] { typeof(Vector128<Int16>) })
                                     .Invoke(null, new object[] {
                                        Sse2.LoadAlignedVector128((Int16*)(_dataTable.inArray1Ptr))
                                     });

            ValidateResult(_dataTable.inArray1Ptr, (bool)(result));
        }

        public void RunClsVarScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClsVarScenario));

            var result = Sse41.TestAllOnes(
                _clsVar1
            );

            ValidateResult(_clsVar1, result);
        }

        public void RunClsVarScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClsVarScenario_Load));

            fixed (Vector128<Int16>* pClsVar1 = &_clsVar1)
            {
                var result = Sse41.TestAllOnes(
                    Sse2.LoadVector128((Int16*)(pClsVar1))
                );

                ValidateResult(_clsVar1, result);
            }
        }

        public void RunLclVarScenario_UnsafeRead()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_UnsafeRead));

            var op1 = Unsafe.Read<Vector128<Int16>>(_dataTable.inArray1Ptr);
            var result = Sse41.TestAllOnes(op1);

            ValidateResult(op1, result);
        }

        public void RunLclVarScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_Load));

            var op1 = Sse2.LoadVector128((Int16*)(_dataTable.inArray1Ptr));
            var result = Sse41.TestAllOnes(op1);

            ValidateResult(op1, result);
        }

        public void RunLclVarScenario_LoadAligned()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunLclVarScenario_LoadAligned));

            var op1 = Sse2.LoadAlignedVector128((Int16*)(_dataTable.inArray1Ptr));
            var result = Sse41.TestAllOnes(op1);

            ValidateResult(op1, result);
        }

        public void RunClassLclFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassLclFldScenario));

            var test = new BooleanUnaryOpTest__TestAllOnesInt16();
            var result = Sse41.TestAllOnes(test._fld1);

            ValidateResult(test._fld1, result);
        }

        public void RunClassLclFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassLclFldScenario_Load));

            var test = new BooleanUnaryOpTest__TestAllOnesInt16();

            fixed (Vector128<Int16>* pFld1 = &test._fld1)
            {
                var result = Sse41.TestAllOnes(
                    Sse2.LoadVector128((Int16*)(pFld1))
                );

                ValidateResult(test._fld1, result);
            }
        }

        public void RunClassFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassFldScenario));

            var result = Sse41.TestAllOnes(_fld1);

            ValidateResult(_fld1, result);
        }

        public void RunClassFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunClassFldScenario_Load));

            fixed (Vector128<Int16>* pFld1 = &_fld1)
            {
                var result = Sse41.TestAllOnes(
                    Sse2.LoadVector128((Int16*)(pFld1))
                );

                ValidateResult(_fld1, result);
            }
        }

        public void RunStructLclFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructLclFldScenario));

            var test = TestStruct.Create();
            var result = Sse41.TestAllOnes(test._fld1);
            ValidateResult(test._fld1, result);
        }

        public void RunStructLclFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructLclFldScenario_Load));

            var test = TestStruct.Create();
            var result = Sse41.TestAllOnes(
                Sse2.LoadVector128((Int16*)(&test._fld1))
            );

            ValidateResult(test._fld1, result);
        }

        public void RunStructFldScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructFldScenario));

            var test = TestStruct.Create();
            test.RunStructFldScenario(this);
        }

        public void RunStructFldScenario_Load()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunStructFldScenario_Load));

            var test = TestStruct.Create();
            test.RunStructFldScenario_Load(this);
        }

        public void RunUnsupportedScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunUnsupportedScenario));

            bool succeeded = false;

            try
            {
                RunBasicScenario_UnsafeRead();
            }
            catch (PlatformNotSupportedException)
            {
                succeeded = true;
            }

            if (!succeeded)
            {
                Succeeded = false;
            }
        }

        private void ValidateResult(Vector128<Int16> op1, bool result, [CallerMemberName] string method = "")
        {
            Int16[] inArray1 = new Int16[Op1ElementCount];

            Unsafe.WriteUnaligned(ref Unsafe.As<Int16, byte>(ref inArray1[0]), op1);

            ValidateResult(inArray1, result, method);
        }

        private void ValidateResult(void* op1, bool result, [CallerMemberName] string method = "")
        {
            Int16[] inArray1 = new Int16[Op1ElementCount];

            Unsafe.CopyBlockUnaligned(ref Unsafe.As<Int16, byte>(ref inArray1[0]), ref Unsafe.AsRef<byte>(op1), (uint)Unsafe.SizeOf<Vector128<Int16>>());

            ValidateResult(inArray1, result, method);
        }

        private void ValidateResult(Int16[] value, bool result, [CallerMemberName] string method = "")
        {
            bool succeeded = true;

            var expectedResult = true;

            for (var i = 0; i < Op1ElementCount; i++)
            {
                expectedResult &= ((~value[i] & -1) == 0);
            }

            succeeded = (expectedResult == result);

            if (!succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"{nameof(Sse41)}.{nameof(Sse41.TestAllOnes)}<Int16>(Vector128<Int16>): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"   value: ({string.Join(", ", value)})");
                TestLibrary.TestFramework.LogInformation($"  result: ({result})");
                TestLibrary.TestFramework.LogInformation(string.Empty);

                Succeeded = false;
            }
        }
    }
}
