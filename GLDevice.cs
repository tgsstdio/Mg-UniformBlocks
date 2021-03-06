﻿using System;
using System.Collections.Generic;

namespace Magnesium.OpenGL
{
    class GLDevice : IMgDevice
    {
        public Result AcquireNextImageKHR(IMgSwapchainKHR swapchain, ulong timeout, IMgSemaphore semaphore, IMgFence fence, out uint pImageIndex)
        {
            throw new NotImplementedException();
        }

        public Result AllocateCommandBuffers(MgCommandBufferAllocateInfo pAllocateInfo, IMgCommandBuffer[] pCommandBuffers)
        {
            throw new NotImplementedException();
        }

        public Result AllocateDescriptorSets(MgDescriptorSetAllocateInfo pAllocateInfo, out IMgDescriptorSet[] pDescriptorSets)
        {
			IGLDescriptorSetAllocator allocator = null;
			return allocator.AllocateDescriptorSets(pAllocateInfo, out pDescriptorSets);
        }

        public Result AllocateMemory(MgMemoryAllocateInfo pAllocateInfo, IMgAllocationCallbacks allocator, out IMgDeviceMemory pMemory)
        {
            throw new NotImplementedException();
        }

        public Result CreateBuffer(MgBufferCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgBuffer pBuffer)
        {
            throw new NotImplementedException();
        }

        public Result CreateBufferView(MgBufferViewCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgBufferView pView)
        {
            throw new NotImplementedException();
        }

        public Result CreateCommandPool(MgCommandPoolCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgCommandPool pCommandPool)
        {
            throw new NotImplementedException();
        }

        public Result CreateComputePipelines(IMgPipelineCache pipelineCache, MgComputePipelineCreateInfo[] pCreateInfos, IMgAllocationCallbacks allocator, out IMgPipeline[] pPipelines)
        {
            throw new NotImplementedException();
        }

		readonly IGLImageDescriptorEntrypoint mImageDescriptor;
        public Result CreateDescriptorPool(MgDescriptorPoolCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgDescriptorPool pDescriptorPool)
        {
			pDescriptorPool = new GLNextDescriptorPool(pCreateInfo, mImageDescriptor);
			return Result.SUCCESS;
        }

        public Result CreateDescriptorSetLayout(MgDescriptorSetLayoutCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgDescriptorSetLayout pSetLayout)
        {
            if (pCreateInfo == null)
            {
                throw new ArgumentNullException(nameof(pCreateInfo));
            }
            pSetLayout = new GLDescriptorSetLayout(pCreateInfo);
            return Result.SUCCESS;
        }

        public Result CreateEvent(MgEventCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgEvent @event)
        {
            throw new NotImplementedException();
        }

        public Result CreateFence(MgFenceCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgFence fence)
        {
            throw new NotImplementedException();
        }

        public Result CreateFramebuffer(MgFramebufferCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgFramebuffer pFramebuffer)
        {
            throw new NotImplementedException();
        }

        public Result CreateGraphicsPipelines(IMgPipelineCache pipelineCache, MgGraphicsPipelineCreateInfo[] pCreateInfos, IMgAllocationCallbacks allocator, out IMgPipeline[] pPipelines)
        {
			var noOfPipelines = pCreateInfos.Length;
			pPipelines = new IMgPipeline[noOfPipelines];
			for (var i = 0; i < noOfPipelines; i += 1)
			{
				var createInfo = pCreateInfos[i];

				var bLayout = (IGLPipelineLayout) createInfo.Layout;

				var programId = 0;
				IGLShaderModuleInspector inspector = null;

				var blocks = inspector.Inspect(programId);
				var arrayMapper = new GLInternalCacheArrayMapper(bLayout, blocks);
				var cache = new GLInternalCache(bLayout, blocks, arrayMapper);



			}

			return Result.SUCCESS;
        }

        public Result CreateImage(MgImageCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgImage pImage)
        {
            throw new NotImplementedException();
        }

        public Result CreateImageView(MgImageViewCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgImageView pView)
        {
            throw new NotImplementedException();
        }

        public Result CreatePipelineCache(MgPipelineCacheCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgPipelineCache pPipelineCache)
        {
            throw new NotImplementedException();
        }

        public Result CreatePipelineLayout(MgPipelineLayoutCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgPipelineLayout pPipelineLayout)
        {
            throw new NotImplementedException();
        }

        public Result CreateQueryPool(MgQueryPoolCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgQueryPool queryPool)
        {
            throw new NotImplementedException();
        }

        public Result CreateRenderPass(MgRenderPassCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgRenderPass pRenderPass)
        {
            throw new NotImplementedException();
        }

        public Result CreateSampler(MgSamplerCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgSampler pSampler)
        {
            throw new NotImplementedException();
        }

        public Result CreateSemaphore(MgSemaphoreCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgSemaphore pSemaphore)
        {
            throw new NotImplementedException();
        }

        public Result CreateShaderModule(MgShaderModuleCreateInfo pCreateInfo, IMgAllocationCallbacks allocator, out IMgShaderModule pShaderModule)
        {
            throw new NotImplementedException();
        }

        public Result CreateSharedSwapchainsKHR(MgSwapchainCreateInfoKHR[] pCreateInfos, IMgAllocationCallbacks allocator, out IMgSwapchainKHR[] pSwapchains)
        {
            throw new NotImplementedException();
        }

        public Result CreateSwapchainKHR(MgSwapchainCreateInfoKHR pCreateInfo, IMgAllocationCallbacks allocator, out IMgSwapchainKHR pSwapchain)
        {
            throw new NotImplementedException();
        }

        public void DestroyDevice(IMgAllocationCallbacks allocator)
        {
            throw new NotImplementedException();
        }

        public Result DeviceWaitIdle()
        {
            throw new NotImplementedException();
        }

        public Result FlushMappedMemoryRanges(MgMappedMemoryRange[] pMemoryRanges)
        {
            throw new NotImplementedException();
        }

        public void FreeCommandBuffers(IMgCommandPool commandPool, IMgCommandBuffer[] pCommandBuffers)
        {
            throw new NotImplementedException();
        }

        public Result FreeDescriptorSets(IMgDescriptorPool descriptorPool, IMgDescriptorSet[] pDescriptorSets)
        {
            throw new NotImplementedException();
        }

        public void GetBufferMemoryRequirements(IMgBuffer buffer, out MgMemoryRequirements pMemoryRequirements)
        {
            throw new NotImplementedException();
        }

        public void GetDeviceMemoryCommitment(IMgDeviceMemory memory, ref ulong pCommittedMemoryInBytes)
        {
            throw new NotImplementedException();
        }

        public PFN_vkVoidFunction GetDeviceProcAddr(string pName)
        {
            throw new NotImplementedException();
        }

        public void GetDeviceQueue(uint queueFamilyIndex, uint queueIndex, out IMgQueue pQueue)
        {
            throw new NotImplementedException();
        }

        public Result GetFenceStatus(IMgFence fence)
        {
            throw new NotImplementedException();
        }

        public void GetImageMemoryRequirements(IMgImage image, out MgMemoryRequirements memoryRequirements)
        {
            throw new NotImplementedException();
        }

        public void GetImageSparseMemoryRequirements(IMgImage image, out MgSparseImageMemoryRequirements[] sparseMemoryRequirements)
        {
            throw new NotImplementedException();
        }

        public void GetImageSubresourceLayout(IMgImage image, MgImageSubresource pSubresource, out MgSubresourceLayout pLayout)
        {
            throw new NotImplementedException();
        }

        public Result GetPipelineCacheData(IMgPipelineCache pipelineCache, out byte[] pData)
        {
            throw new NotImplementedException();
        }

        public Result GetQueryPoolResults(IMgQueryPool queryPool, uint firstQuery, uint queryCount, IntPtr dataSize, IntPtr pData, ulong stride, MgQueryResultFlagBits flags)
        {
            throw new NotImplementedException();
        }

        public void GetRenderAreaGranularity(IMgRenderPass renderPass, out MgExtent2D pGranularity)
        {
            throw new NotImplementedException();
        }

        public Result GetSwapchainImagesKHR(IMgSwapchainKHR swapchain, out IMgImage[] pSwapchainImages)
        {
            throw new NotImplementedException();
        }

        public Result InvalidateMappedMemoryRanges(MgMappedMemoryRange[] pMemoryRanges)
        {
            throw new NotImplementedException();
        }

        public Result MergePipelineCaches(IMgPipelineCache dstCache, IMgPipelineCache[] pSrcCaches)
        {
            throw new NotImplementedException();
        }

        public Result ResetFences(IMgFence[] pFences)
        {
            throw new NotImplementedException();
        }

        private IGLDeviceEntrypoint mEntrypoint;
        public void UpdateDescriptorSets(MgWriteDescriptorSet[] pDescriptorWrites, MgCopyDescriptorSet[] pDescriptorCopies)
        {

        }

        public Result WaitForFences(IMgFence[] pFences, bool waitAll, ulong timeout)
        {
            throw new NotImplementedException();
        }
    }
}
